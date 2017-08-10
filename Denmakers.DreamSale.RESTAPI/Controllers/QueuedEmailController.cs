using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Messages;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/QueuedEmails")]
    [Infrastructure.Securities.AdminAuthorize]
    public partial class QueuedEmailController : ApiControllerBase
    {
        #region Fields
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        #endregion

        #region Ctor
        public QueuedEmailController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IQueuedEmailService queuedEmailService, ILocalizationService localizationService, IDateTimeHelper dateTimeHelper, IPermissionService permissionService)
            : base(baseService, logger, webHelper)
        {
            this._queuedEmailService = queuedEmailService;
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;
            this._permissionService = permissionService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("QueuedEmailSearchModel", Name = "QueuedEmailSearchModel")]
        public HttpResponseMessage QueuedEmailSearchModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    var model = new QueuedEmailListVM
                    {
                        //default value
                        SearchMaxSentTries = 10
                    };

                    response = request.CreateResponse<QueuedEmailListVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("", Name = "QueuedEmailList")]
        public HttpResponseMessage QueuedEmailList(HttpRequestMessage request, QueuedEmailListVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    DateTime? startDateValue = (model.SearchStartDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.SearchStartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.SearchEndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.SearchEndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    var queuedEmails = _queuedEmailService.SearchEmails(model.SearchFromEmail, model.SearchToEmail,
                        startDateValue, endDateValue, model.SearchLoadNotSent, false, model.SearchMaxSentTries, true, pageIndex, pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = queuedEmails.Select(x => {
                            var m = x.ToModel();
                            m.PriorityName = x.Priority.GetLocalizedEnum(_localizationService, _baseService.WorkContext);
                            m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                            if (x.DontSendBeforeDateUtc.HasValue)
                                m.DontSendBeforeDate = _dateTimeHelper.ConvertToUserTime(x.DontSendBeforeDateUtc.Value, DateTimeKind.Utc);
                            if (x.SentOnUtc.HasValue)
                                m.SentOn = _dateTimeHelper.ConvertToUserTime(x.SentOnUtc.Value, DateTimeKind.Utc);

                            //little performance optimization: ensure that "Body" is not returned
                            m.Body = "";

                            return m;
                        }),
                        Total = queuedEmails.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetQueuedEmailById")]
        public HttpResponseMessage GetQueuedEmailById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    var email = _queuedEmailService.GetQueuedEmailById(id);
                    if (email == null)
                    {
                        //No email found with the specified id
                        Url.Route("QueuedEmailSearchModel", null);
                        string uri = Url.Link("QueuedEmailSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = email.ToModel();
                    model.PriorityName = email.Priority.GetLocalizedEnum(_localizationService, _baseService.WorkContext);
                    model.CreatedOn = _dateTimeHelper.ConvertToUserTime(email.CreatedOnUtc, DateTimeKind.Utc);
                    if (email.SentOnUtc.HasValue)
                        model.SentOn = _dateTimeHelper.ConvertToUserTime(email.SentOnUtc.Value, DateTimeKind.Utc);
                    if (email.DontSendBeforeDateUtc.HasValue)
                        model.DontSendBeforeDate = _dateTimeHelper.ConvertToUserTime(email.DontSendBeforeDateUtc.Value, DateTimeKind.Utc);
                    else model.SendImmediately = true;

                    response = request.CreateResponse<QueuedEmailVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteAllQueuedEmail", Name = "DeleteAllQueuedEmail")]
        public HttpResponseMessage DeleteAllQueuedEmail(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    _queuedEmailService.DeleteAllEmails();
                    _baseService.Commit();

                    Url.Route("QueuedEmailSearchModel", null);
                    string uri = Url.Link("QueuedEmailSearchModel", null);
                    response.Headers.Location = new Uri(uri);
                    return response;
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteQueuedEmail", Name = "DeleteQueuedEmail")]
        public HttpResponseMessage DeleteQueuedEmail(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    var email = _queuedEmailService.GetQueuedEmailById(id);
                    if (email == null)
                    {
                        //No email found with the specified id
                        Url.Route("QueuedEmailSearchModel", null);
                        string uri = Url.Link("QueuedEmailSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    _queuedEmailService.DeleteQueuedEmail(email);
                    _baseService.Commit();
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteSelectedQueuedEmail", Name = "DeleteSelectedQueuedEmail")]
        public HttpResponseMessage DeleteSelectedQueuedEmail(HttpRequestMessage request, ICollection<int> selectedIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, new { Result = false });
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    if (selectedIds != null)
                    {
                        _queuedEmailService.DeleteQueuedEmails(_queuedEmailService.GetQueuedEmailsByIds(selectedIds.ToArray()));
                        _baseService.Commit();
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, new { Result = true });
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("Requeue", Name = "Requeue")]
        public HttpResponseMessage Requeue(HttpRequestMessage request, QueuedEmailVM queuedEmailModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    var queuedEmail = _queuedEmailService.GetQueuedEmailById(queuedEmailModel.Id);
                    if (queuedEmail == null)
                    {
                        //No email found with the specified id
                        Url.Route("QueuedEmailSearchModel", null);
                        string nuri = Url.Link("QueuedEmailSearchModel", null);
                        response.Headers.Location = new Uri(nuri);
                        return response;
                    }

                    var requeuedEmail = new QueuedEmail
                    {
                        PriorityId = queuedEmail.PriorityId,
                        From = queuedEmail.From,
                        FromName = queuedEmail.FromName,
                        To = queuedEmail.To,
                        ToName = queuedEmail.ToName,
                        ReplyTo = queuedEmail.ReplyTo,
                        ReplyToName = queuedEmail.ReplyToName,
                        CC = queuedEmail.CC,
                        Bcc = queuedEmail.Bcc,
                        Subject = queuedEmail.Subject,
                        Body = queuedEmail.Body,
                        AttachmentFilePath = queuedEmail.AttachmentFilePath,
                        AttachmentFileName = queuedEmail.AttachmentFileName,
                        AttachedDownloadId = queuedEmail.AttachedDownloadId,
                        CreatedOnUtc = DateTime.UtcNow,
                        EmailAccountId = queuedEmail.EmailAccountId,
                        DontSendBeforeDateUtc = (queuedEmailModel.SendImmediately || !queuedEmailModel.DontSendBeforeDate.HasValue) ?
                            null : (DateTime?)_dateTimeHelper.ConvertToUtcTime(queuedEmailModel.DontSendBeforeDate.Value)
                    };
                    _queuedEmailService.InsertQueuedEmail(requeuedEmail);
                    _baseService.Commit();

                    Url.Route("GetQueuedEmailById", new { id = requeuedEmail.Id });
                    string newUri = Url.Link("GetQueuedEmailById", new { id = requeuedEmail.Id });
                    response.Headers.Location = new Uri(newUri);
                    return response;
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("EditQueuedEmail", Name = "EditQueuedEmail")]
        public HttpResponseMessage EditQueuedEmail(HttpRequestMessage request, QueuedEmailVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageMessageQueue))
                {
                    var email = _queuedEmailService.GetQueuedEmailById(model.Id);
                    if (email == null)
                    {
                        //No email found with the specified id
                        Url.Route("QueuedEmailSearchModel", null);
                        string nuri = Url.Link("QueuedEmailSearchModel", null);
                        response.Headers.Location = new Uri(nuri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        email = model.ToEntity(email);
                        email.DontSendBeforeDateUtc = (model.SendImmediately || !model.DontSendBeforeDate.HasValue) ?
                            null : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.DontSendBeforeDate.Value);
                        _queuedEmailService.UpdateQueuedEmail(email);
                        _baseService.Commit();

                        //SuccessNotification(_localizationService.GetResource("Admin.System.QueuedEmails.Updated"));
                        if (continueEditing)
                        {
                            Url.Route("GetQueuedEmailById", new { id = email.Id });
                            string nUri = Url.Link("GetQueuedEmailById", new { id = email.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("QueuedEmailSearchModel", null);
                            string nuri = Url.Link("QueuedEmailSearchModel", null);
                            response.Headers.Location = new Uri(nuri);
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        model.PriorityName = email.Priority.GetLocalizedEnum(_localizationService, _baseService.WorkContext);
                        model.CreatedOn = _dateTimeHelper.ConvertToUserTime(email.CreatedOnUtc, DateTimeKind.Utc);
                        if (email.SentOnUtc.HasValue)
                            model.SentOn = _dateTimeHelper.ConvertToUserTime(email.SentOnUtc.Value, DateTimeKind.Utc);
                        if (email.DontSendBeforeDateUtc.HasValue)
                            model.DontSendBeforeDate = _dateTimeHelper.ConvertToUserTime(email.DontSendBeforeDateUtc.Value, DateTimeKind.Utc);

                        response = request.CreateResponse<QueuedEmailVM>(HttpStatusCode.OK, model);
                    }
                    
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        #endregion
    }
}
