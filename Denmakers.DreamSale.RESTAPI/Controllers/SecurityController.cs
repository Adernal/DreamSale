using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using Denmakers.DreamSale.ViewModels.AdminVM.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Security")]
    //[Infrastructure.Securities.AdminAuthorize]
    public class SecurityController : ApiControllerBase
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructors

        public SecurityController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IPermissionService permissionService, ICustomerService customerService, ILocalizationService localizationService)
            : base(baseService, logger, webHelper)
        {
            this._permissionService = permissionService;
            this._customerService = customerService;
            this._localizationService = localizationService;
        }

        #endregion

        #region Methods
        [HttpGet]
        [Route("AccessDenied", Name = "AccessDenied")]
        public HttpResponseMessage AccessDenied(HttpRequestMessage request, string pageUrl)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                var currentCustomer = _baseService.WorkContext.CurrentCustomer;
                if (currentCustomer == null || currentCustomer.IsGuest())
                {
                    _logger.Information(string.Format("Access denied to anonymous request on {0}", pageUrl));
                    _baseService.Commit();
                    response = request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation);
                }
                else
                {
                    _logger.Information(string.Format("Access denied to user #{0} '{1}' on {2}", currentCustomer.Email, currentCustomer.Email, pageUrl));
                    _baseService.Commit();
                    response = request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation);
                }                
                return response;

            });
        }

        [HttpGet]
        [Route("Permissions", Name = "Permissions")]
        public HttpResponseMessage Permissions(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = new PermissionMappingVM();

                    var permissionRecords = _permissionService.GetAllPermissionRecords();
                    var customerRoles = _customerService.GetAllCustomerRoles(true);
                    foreach (var pr in permissionRecords)
                    {
                        model.AvailablePermissions.Add(new PermissionRecordVM
                        {
                            //Name = pr.Name,
                            Name = pr.GetLocalizedPermissionName(_localizationService, _baseService.WorkContext),
                            SystemName = pr.SystemName
                        });
                    }
                    foreach (var cr in customerRoles)
                    {
                        model.AvailableCustomerRoles.Add(new CustomerRoleVM
                        {
                            Id = cr.Id,
                            Name = cr.Name
                        });
                    }
                    foreach (var pr in permissionRecords)
                        foreach (var cr in customerRoles)
                        {
                            bool allowed = pr.CustomerRoles.Count(x => x.Id == cr.Id) > 0;
                            if (!model.Allowed.ContainsKey(pr.SystemName))
                                model.Allowed[pr.SystemName] = new Dictionary<int, bool>();
                            model.Allowed[pr.SystemName][cr.Id] = allowed;
                        }

                    response = request.CreateResponse<PermissionMappingVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    return AccessDeniedView(request);
                    //response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("PermissionsSave", Name = "PermissionsSave")]
        public HttpResponseMessage PermissionsSave(HttpRequestMessage request, System.Web.Mvc.FormCollection form)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var permissionRecords = _permissionService.GetAllPermissionRecords();
                    var customerRoles = _customerService.GetAllCustomerRoles(true);


                    foreach (var cr in customerRoles)
                    {
                        string formKey = "allow_" + cr.Id;
                        var permissionRecordSystemNamesToRestrict = form[formKey] != null ? form[formKey].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>();

                        foreach (var pr in permissionRecords)
                        {

                            bool allow = permissionRecordSystemNamesToRestrict.Contains(pr.SystemName);
                            if (allow)
                            {
                                if (pr.CustomerRoles.FirstOrDefault(x => x.Id == cr.Id) == null)
                                {
                                    pr.CustomerRoles.Add(cr);
                                    _permissionService.UpdatePermissionRecord(pr);
                                }
                            }
                            else
                            {
                                if (pr.CustomerRoles.FirstOrDefault(x => x.Id == cr.Id) != null)
                                {
                                    pr.CustomerRoles.Remove(cr);
                                    _permissionService.UpdatePermissionRecord(pr);
                                }
                            }
                        }
                    }

                    //SuccessNotification(_localizationService.GetResource("Admin.Configuration.ACL.Updated"));
                    Url.Route("Permissions", null);
                    string uri = Url.Link("Permissions", null);
                    response.Headers.Location = new Uri(uri);
                }
                else
                {
                    return AccessDeniedView(request);
                }
                return response;

            });
        }

        #endregion
    }
}
