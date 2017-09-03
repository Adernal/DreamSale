using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Messages;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/EmailAccounts")]
    //[Infrastructure.Securities.AdminAuthorize]
    public partial class EmailAccountController : ApiControllerBase
    {
        #region Fields
        private readonly IEmailAccountService _emailAccountService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IEmailSender _emailSender;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly EmailAccountSettings _emailAccountSettings;
        #endregion

        #region Ctor
        public EmailAccountController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IEmailAccountService emailAccountService,
            ILocalizationService localizationService, ISettingService settingService,
            IEmailSender emailSender, IStoreContext storeContext,
            IPermissionService permissionService,
            ICustomerActivityService customerActivityService)
            : base(baseService, logger, webHelper)
        {
            this._emailAccountService = emailAccountService;
            this._localizationService = localizationService;
            this._emailSender = emailSender;
            this._storeContext = storeContext;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
            this._settingService = settingService;
            this._emailAccountSettings = settingService.LoadSetting<EmailAccountSettings>();
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("", Name = "EmailList")]
        public HttpResponseMessage EmailList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccountModels = _emailAccountService.GetAllEmailAccounts()
                                    .Select(x => x.ToModel())
                                    .ToList();
                    foreach (var eam in emailAccountModels)
                        eam.IsDefaultEmailAccount = eam.Id == _emailAccountSettings.DefaultEmailAccountId;

                    var gridModel = new DataSourceResult
                    {
                        Data = emailAccountModels,
                        Total = emailAccountModels.Count()
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
        [Route("{id:int}", Name = "GetEmailById")]
        public HttpResponseMessage GetEmailById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(id);
                    if (emailAccount == null)
                    {
                        //No email account found with the specified id
                        Url.Route("EmailList", null);
                        string uri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                    response = request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, emailAccount.ToModel());
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
        [Route("MarkAsDefaultEmail/{id:int}", Name = "MarkAsDefaultEmail")]
        public HttpResponseMessage MarkAsDefaultEmail(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var defaultEmailAccount = _emailAccountService.GetEmailAccountById(id);
                    if (defaultEmailAccount != null)
                    {
                        _emailAccountSettings.DefaultEmailAccountId = defaultEmailAccount.Id;
                        _settingService.SaveSetting(_emailAccountSettings);
                        _baseService.Commit();
                    }
                    Url.Route("EmailList", null);
                    string uri = Url.Link("EmailList", null);
                    response.Headers.Location = new Uri(uri);
                }
                else
                {
                    response = AccessDeniedView(request);
                   // response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpGet]
        [Route("CreateEmailModel", Name = "CreateEmailModel")]
        public HttpResponseMessage CreateEmailModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var model = new EmailAccountVM();
                    //default values
                    model.Port = 25;
                    response = request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, model);
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
        [Route("CreateEmail", Name = "CreateEmail")]
        public HttpResponseMessage CreateEmail(HttpRequestMessage request, EmailAccountVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    if (ModelState.IsValid)
                    {
                        var emailAccount = model.ToEntity();
                        //set password manually
                        emailAccount.Password = model.Password;
                        _emailAccountService.InsertEmailAccount(emailAccount);

                        //activity log
                        _customerActivityService.InsertActivity("AddNewEmailAccount", _localizationService.GetResource("ActivityLog.AddNewEmailAccount"), emailAccount.Id);

                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.EmailAccounts.Added"));

                        if (continueEditing)
                        {
                            Url.Route("GetEmailById", new { id = emailAccount.Id });
                            string nUri = Url.Link("GetEmailById", new { id = emailAccount.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("EmailList", null);
                            string nuri = Url.Link("EmailList", null);
                            response.Headers.Location = new Uri(nuri);

                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        response = request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, model);
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
        [Route("EditEmail", Name = "EditEmail")]
        public HttpResponseMessage EditEmail(HttpRequestMessage request, EmailAccountVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(model.Id);
                    if (emailAccount == null)
                    {
                        //No email account found with the specified id
                        Url.Route("EmailList", null);
                        string uri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        emailAccount = model.ToEntity(emailAccount);
                        _emailAccountService.UpdateEmailAccount(emailAccount);

                        //activity log
                        _customerActivityService.InsertActivity("EditEmailAccount", _localizationService.GetResource("ActivityLog.EditEmailAccount"), emailAccount.Id);

                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.EmailAccounts.Updated"));
                        if (continueEditing)
                        {
                            Url.Route("GetEmailById", new { id = emailAccount.Id });
                            string nUri = Url.Link("GetEmailById", new { id = emailAccount.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("EmailList", null);
                            string nuri = Url.Link("EmailList", null);
                            response.Headers.Location = new Uri(nuri);
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        response = request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, model);
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
        [Route("ChangePassword")]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, EmailAccountVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(model.Id);
                    if (emailAccount == null)
                    {
                        //No email account found with the specified id
                        Url.Route("EmailList", null);
                        string uri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                    //do not validate model
                    emailAccount.Password = model.Password;
                    _emailAccountService.UpdateEmailAccount(emailAccount);

                    _baseService.Commit();
                    //SuccessNotification(_localizationService.GetResource("Admin.Configuration.EmailAccounts.Fields.Password.PasswordChanged"));

                    Url.Route("GetEmailById", new { id = emailAccount.Id });
                    string nUri = Url.Link("GetEmailById", new { id = emailAccount.Id });
                    response.Headers.Location = new Uri(nUri);
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
        [Route("DeleteEmailAccount")]
        public HttpResponseMessage DeleteEmailAccount(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(id);
                    if (emailAccount == null)
                    {
                        //No email account found with the specified id
                        Url.Route("EmailList", null);
                        string uri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    try
                    {
                        _emailAccountService.DeleteEmailAccount(emailAccount);

                        //activity log
                        _customerActivityService.InsertActivity("DeleteEmailAccount", _localizationService.GetResource("ActivityLog.DeleteEmailAccount"), emailAccount.Id);

                        _baseService.Commit();

                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.EmailAccounts.Deleted"));

                        Url.Route("EmailList", null);
                        string nUri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(nUri);
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        Url.Route("GetEmailById", new { id = emailAccount.Id });
                        string nUri = Url.Link("GetEmailById", new { id = emailAccount.Id });
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
        [Route("SendTestEmail")]
        public HttpResponseMessage SendTestEmail(HttpRequestMessage request, EmailAccountVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(model.Id);
                    if (emailAccount == null)
                    {
                        //No email account found with the specified id
                        Url.Route("EmailList", null);
                        string uri = Url.Link("EmailList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (!CommonHelper.IsValidEmail(model.SendTestEmailTo))
                    {
                        LogError(_localizationService.GetResource("Admin.Common.WrongEmail"));

                        response = request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, model);
                        return response;
                    }

                    try
                    {
                        if (String.IsNullOrWhiteSpace(model.SendTestEmailTo))
                            throw new DreamSaleException("Enter test email address");

                        string subject = _storeContext.CurrentStore.Name + ". Testing email functionality.";
                        string body = "Email works fine.";
                        _emailSender.SendEmail(emailAccount, subject, body, emailAccount.Email, emailAccount.DisplayName, model.SendTestEmailTo, null);

                        _baseService.Commit();
                        return request.CreateResponse(HttpStatusCode.OK);
                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.EmailAccounts.SendTestEmail.Success"), false);
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        return request.CreateResponse<EmailAccountVM>(HttpStatusCode.OK, model);
                    }
                }
                else
                {
                    return AccessDeniedView(request);
                    // response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }

            });
        }

        #endregion
    }
}
