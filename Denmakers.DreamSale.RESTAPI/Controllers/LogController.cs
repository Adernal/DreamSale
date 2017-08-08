using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Logs")]
    public class LogController : ApiControllerBase
    {
        #region Fields
        private readonly ILocalizationService _localizationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPermissionService _permissionService;
        #endregion

        #region Ctor
        public LogController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ILocalizationService localizationService, IDateTimeHelper dateTimeHelper, IPermissionService permissionService)
            : base(baseService, logger, webHelper)
        {
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;
            this._permissionService = permissionService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("LogSearchModel", Name = "LogSearchModel")]
        public HttpResponseMessage LogSearchModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    var model = new LogListVM();
                    model.AvailableLogLevels = LogLevel.Debug.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailableLogLevels.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

                    response = request.CreateResponse<LogListVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("", Name = "LogList")]
        public HttpResponseMessage LogList(HttpRequestMessage request, LogListVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    DateTime? createdOnFromValue = (model.CreatedOnFrom == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.CreatedOnFrom.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? createdToFromValue = (model.CreatedOnTo == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.CreatedOnTo.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    LogLevel? logLevel = model.LogLevelId > 0 ? (LogLevel?)(model.LogLevelId) : null;


                    var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message, logLevel, pageIndex, pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = logItems.Select(x => new LogVM
                        {
                            Id = x.Id,
                            LogLevel = x.LogLevel.GetLocalizedEnum(_localizationService, _baseService.WorkContext),
                            ShortMessage = x.ShortMessage,
                            //little performance optimization: ensure that "FullMessage" is not returned
                            FullMessage = "",
                            IpAddress = x.IpAddress,
                            CustomerId = x.CustomerId,
                            CustomerEmail = x.Customer != null ? x.Customer.Email : null,
                            PageUrl = x.PageUrl,
                            ReferrerUrl = x.ReferrerUrl,
                            CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc)
                        }),
                        Total = logItems.TotalCount
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
        [Route("{id:int}", Name = "GetLogById")]
        public HttpResponseMessage GetLogById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    var log = _logger.GetLogById(id);
                    if (log == null)
                    {
                        //No log found with the specified id
                        Url.Route("LogSearchModel", null);
                        string uri = uri = Url.Link("LogSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new LogVM
                    {
                        Id = log.Id,
                        LogLevel = log.LogLevel.GetLocalizedEnum(_localizationService, _baseService.WorkContext),
                        ShortMessage = log.ShortMessage,
                        FullMessage = log.FullMessage,
                        IpAddress = log.IpAddress,
                        CustomerId = log.CustomerId,
                        CustomerEmail = log.Customer != null ? log.Customer.Email : null,
                        PageUrl = log.PageUrl,
                        ReferrerUrl = log.ReferrerUrl,
                        CreatedOn = _dateTimeHelper.ConvertToUserTime(log.CreatedOnUtc, DateTimeKind.Utc)
                    };

                    response = request.CreateResponse<LogVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ClearAllLogs", Name = "ClearAllLogs")]
        public HttpResponseMessage ClearAllLogs(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    _logger.ClearLog();
                    _baseService.Commit();

                    Url.Route("LogSearchModel", null);
                    string uri = uri = Url.Link("LogSearchModel", null);
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
        [Route("DeleteLog", Name = "DeleteLog")]
        public HttpResponseMessage DeleteLog(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    var log = _logger.GetLogById(id);
                    if (log == null)
                    {
                        //No log found with the specified id
                        Url.Route("LogSearchModel", null);
                        string uri = uri = Url.Link("LogSearchModel", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    _logger.DeleteLog(log);
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
        [Route("DeleteSelectedLog", Name = "DeleteSelectedLog")]
        public HttpResponseMessage DeleteSelectedLog(HttpRequestMessage request, ICollection<int> selectedIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, new { Result = false });
                if (_permissionService.Authorize(StandardPermissionProvider.ManageSystemLog))
                {
                    if (selectedIds != null)
                    {
                        _logger.DeleteLogs(_logger.GetLogByIds(selectedIds.ToArray()).ToList());
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
        #endregion
    }
}
