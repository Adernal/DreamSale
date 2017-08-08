using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Messages;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using Denmakers.DreamSale.ViewModels.AdminVM.Stores;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/ReturnRequest")]
    public partial class ReturnRequestController : ApiControllerBase
    {
        #region Fields
        private readonly IReturnRequestService _returnRequestService;
        private readonly IOrderService _orderService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;
        private readonly IDownloadService _downloadService;

        #endregion

        #region Ctor
        public ReturnRequestController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IReturnRequestService returnRequestService,
            IOrderService orderService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            IWorkflowMessageService workflowMessageService,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IDownloadService downloadService)
            : base(baseService, logger, webHelper)
        {
            this._returnRequestService = returnRequestService;
            this._orderService = orderService;
            this._customerService = customerService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._workflowMessageService = workflowMessageService;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
            this._downloadService = downloadService;
        }
        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareReturnRequestModel(ReturnRequestVM model, ReturnRequest returnRequest, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (returnRequest == null)
                throw new ArgumentNullException("returnRequest");

            var orderItem = _orderService.GetOrderItemById(returnRequest.OrderItemId);
            if (orderItem != null)
            {
                model.ProductId = orderItem.ProductId;
                model.ProductName = orderItem.Product.Name;
                model.OrderId = orderItem.OrderId;
                model.AttributeInfo = orderItem.AttributeDescription;
                model.CustomOrderNumber = orderItem.Order.CustomOrderNumber;
            }
            model.Id = returnRequest.Id;
            model.CustomNumber = returnRequest.CustomNumber;
            model.CustomerId = returnRequest.CustomerId;
            var customer = returnRequest.Customer;
            model.CustomerInfo = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
            model.Quantity = returnRequest.Quantity;
            model.ReturnRequestStatusStr = returnRequest.ReturnRequestStatus.GetLocalizedEnum(_localizationService, _baseService.WorkContext);

            var download = _downloadService.GetDownloadById(returnRequest.UploadedFileId);
            model.UploadedFileGuid = download != null ? download.DownloadGuid : Guid.Empty;
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(returnRequest.CreatedOnUtc, DateTimeKind.Utc);
            if (!excludeProperties)
            {
                model.ReasonForReturn = returnRequest.ReasonForReturn;
                model.RequestedAction = returnRequest.RequestedAction;
                model.CustomerComments = returnRequest.CustomerComments;
                model.StaffNotes = returnRequest.StaffNotes;
                model.ReturnRequestStatusId = returnRequest.ReturnRequestStatusId;
            }
        }

        #endregion

        #region Methods
        [HttpGet]
        [Route("ReturnRequestSearchModel", Name = "ReturnRequestSearchModel")]
        public HttpResponseMessage ReturnRequestSearchModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var model = new ReturnRequestListVM
                    {
                        ReturnRequestStatusList = ReturnRequestStatus.Cancelled.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList(),
                        ReturnRequestStatusId = -1
                    };

                    model.ReturnRequestStatusList.Insert(0, new System.Web.Mvc.SelectListItem
                    {
                        Value = "-1",
                        Text = _localizationService.GetResource("Admin.ReturnRequests.SearchReturnRequestStatus.All"),
                        Selected = true
                    });


                    response = request.CreateResponse<ReturnRequestListVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    //response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                    response = AccessDeniedView(request);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ReturnRequestList", Name = "ReturnRequestList")]
        public HttpResponseMessage ReturnRequestList(HttpRequestMessage request, ReturnRequestListVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var rrs = model.ReturnRequestStatusId == -1 ? null : (ReturnRequestStatus?)model.ReturnRequestStatusId;

                    var startDateValue = model.StartDate == null ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    var endDateValue = model.EndDate == null ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    var returnRequests = _returnRequestService.SearchReturnRequests(0, 0, 0, model.CustomNumber, rrs, startDateValue, endDateValue, pageIndex, pageSize);

                    var returnRequestModels = new List<ReturnRequestVM>();
                    foreach (var rr in returnRequests)
                    {
                        var m = new ReturnRequestVM();
                        PrepareReturnRequestModel(m, rr, false);
                        returnRequestModels.Add(m);
                    }
                    var gridModel = new DataSourceResult
                    {
                        Data = returnRequestModels,
                        Total = returnRequests.TotalCount,
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    //response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                    response = AccessDeniedView(request);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetReturnRequestById")]
        public HttpResponseMessage GetReturnRequestById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var returnRequest = _returnRequestService.GetReturnRequestById(id);
                    if (returnRequest == null)
                    {
                        //No return request found with the specified id
                        Url.Route("ReturnRequestSearchModel", null);
                        string uri = Url.Link("ReturnRequestSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new ReturnRequestVM();
                    PrepareReturnRequestModel(model, returnRequest, false);

                    response = request.CreateResponse<ReturnRequestVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = AccessDeniedView(request);
                    //response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("EditReturnRequest", Name = "EditReturnRequest")]
        public HttpResponseMessage EditReturnRequest(HttpRequestMessage request, ReturnRequestVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var returnRequest = _returnRequestService.GetReturnRequestById(model.Id);
                    if (returnRequest == null)
                    {
                        //No return request found with the specified id
                        Url.Route("ReturnRequestSearchModel", null);
                        string uri = Url.Link("ReturnRequestSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        returnRequest.Quantity = model.Quantity;
                        returnRequest.ReasonForReturn = model.ReasonForReturn;
                        returnRequest.RequestedAction = model.RequestedAction;
                        returnRequest.CustomerComments = model.CustomerComments;
                        returnRequest.StaffNotes = model.StaffNotes;
                        returnRequest.ReturnRequestStatusId = model.ReturnRequestStatusId;
                        returnRequest.UpdatedOnUtc = DateTime.UtcNow;
                        _customerService.UpdateCustomer(returnRequest.Customer);

                        //activity log
                        _customerActivityService.InsertActivity("EditReturnRequest", _localizationService.GetResource("ActivityLog.EditReturnRequest"), returnRequest.Id);

                        _baseService.Commit();

                        //SuccessNotification(_localizationService.GetResource("Admin.ReturnRequests.Updated"));
                        if (continueEditing)
                        {
                            Url.Route("GetReturnRequestById", new { id = returnRequest.Id });
                            string nUri = Url.Link("GetReturnRequestById", new { id = returnRequest.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("ReturnRequestSearchModel", null);
                            string nuri = Url.Link("ReturnRequestSearchModel", null);
                            response.Headers.Location = new Uri(nuri);
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        PrepareReturnRequestModel(model, returnRequest, true);
                        response = request.CreateResponse<ReturnRequestVM>(HttpStatusCode.OK, model);
                    }
                }
                else
                {
                    response = AccessDeniedView(request);
                    // response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteReturnRequest")]
        public HttpResponseMessage DeleteReturnRequest(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var returnRequest = _returnRequestService.GetReturnRequestById(id);
                    if (returnRequest == null)
                    {
                        //No return request found with the specified id
                        Url.Route("ReturnRequestSearchModel", null);
                        string uri = Url.Link("ReturnRequestSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    try
                    {
                        _returnRequestService.DeleteReturnRequest(returnRequest);

                        //activity log
                        _customerActivityService.InsertActivity("DeleteReturnRequest", _localizationService.GetResource("ActivityLog.DeleteReturnRequest"), returnRequest.Id);

                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.ReturnRequests.Deleted"));

                        Url.Route("ReturnRequestSearchModel", null);
                        string nUri = Url.Link("ReturnRequestSearchModel", null);
                        response.Headers.Location = new Uri(nUri);
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        Url.Route("GetReturnRequestById", new { id = returnRequest.Id });
                        string nUri = Url.Link("GetReturnRequestById", new { id = returnRequest.Id });
                        response.Headers.Location = new Uri(nUri);
                    }
                    
                }
                else
                {
                    response = AccessDeniedView(request);
                    // response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("NotifyCustomer", Name = "NotifyCustomer")]
        public HttpResponseMessage NotifyCustomer(HttpRequestMessage request, ReturnRequestVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageReturnRequests))
                {
                    var returnRequest = _returnRequestService.GetReturnRequestById(model.Id);
                    if (returnRequest == null)
                    {
                        //No return request found with the specified id
                        Url.Route("ReturnRequestSearchModel", null);
                        string uri = Url.Link("ReturnRequestSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var orderItem = _orderService.GetOrderItemById(returnRequest.OrderItemId);
                    if (orderItem == null)
                    {
                        LogError(_localizationService.GetResource("Admin.ReturnRequests.OrderItemDeleted"));
                        Url.Route("GetReturnRequestById", new { id = returnRequest.Id });
                        string nUri = Url.Link("GetReturnRequestById", new { id = returnRequest.Id });
                        response.Headers.Location = new Uri(nUri);
                        return response;
                    }

                    int queuedEmailId = _workflowMessageService.SendReturnRequestStatusChangedCustomerNotification(returnRequest, orderItem, orderItem.Order.CustomerLanguageId);

                    //if (queuedEmailId > 0)
                    //    SuccessNotification(_localizationService.GetResource("Admin.ReturnRequests.Notified"));

                    _baseService.Commit();
                }
                else
                {
                    response = AccessDeniedView(request);
                    // response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }
        #endregion
    }
}
