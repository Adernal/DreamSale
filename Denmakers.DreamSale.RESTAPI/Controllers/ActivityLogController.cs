﻿using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Logging;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/ActivityLog")]
    //[Infrastructure.Securities.AdminAuthorize]
    public class ActivityLogController : ApiControllerBase
    {
        #region Fields
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;

        #endregion Fields

        #region Constructors

        public ActivityLogController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ICustomerActivityService customerActivityService,
            IDateTimeHelper dateTimeHelper, ILocalizationService localizationService,
            IPermissionService permissionService)
            : base(baseService, logger, webHelper)
        {
            this._customerActivityService = customerActivityService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
        }

        #endregion

        #region Activity log types
        [HttpGet]
        [Route("Types", Name = "ActivityListTypes")]
        public HttpResponseMessage ListTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = _customerActivityService
                                .GetAllActivityTypes()
                                .Select(x => x.ToModel())
                                .ToList();
                    response = request.CreateResponse<List<ActivityLogTypeVM>>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage SaveTypes(HttpRequestMessage request, System.Web.Mvc.FormCollection form)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //activity log
                    _customerActivityService.InsertActivity("EditActivityLogTypes", _localizationService.GetResource("ActivityLog.EditActivityLogTypes"));

                    string formKey = "checkbox_activity_types";
                    var checkedActivityTypes = form[formKey] != null ? form[formKey].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList() : new List<int>();

                    var activityTypes = _customerActivityService.GetAllActivityTypes();
                    foreach (var activityType in activityTypes)
                    {
                        activityType.Enabled = checkedActivityTypes.Contains(activityType.Id);
                        _customerActivityService.UpdateActivityType(activityType);
                    }
                    _baseService.Commit();
                    var uri = Url.Link("ActivityListTypes", null);
                    response.Headers.Location = new Uri(uri);
                }
                return response;

            });
        }

        #endregion
    }
}
