using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.ViewModels.AdminVM.Stores;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Stores")]
    [Infrastructure.Securities.AdminAuthorize]
    public partial class StoreController : ApiControllerBase
    {
        #region Fields
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;

        #endregion

        #region Ctor
        public StoreController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IStoreService storeService,
            ISettingService settingService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IPermissionService permissionService,
            ICustomerActivityService customerActivityService)
            : base(baseService, logger, webHelper)
        {
            this._storeService = storeService;
            this._settingService = settingService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
        }
        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareLanguagesModel(StoreVM model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableLanguages.Add(new System.Web.Mvc.SelectListItem
            {
                Text = "---",
                Value = "0"
            });
            var languages = _languageService.GetAllLanguages(true);
            foreach (var language in languages)
            {
                model.AvailableLanguages.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = language.Name,
                    Value = language.Id.ToString()
                });
            }
        }

        [NonAction]
        protected virtual void UpdateAttributeLocales(Store store, StoreVM model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(store,
                    x => x.Name,
                    localized.Name,
                    localized.LanguageId);

                _baseService.Commit();
            }
        }

        #endregion

        #region Methods
        [HttpGet]
        [Route("", Name = "StoreList")]
        public HttpResponseMessage StoreList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    var StoreVMs = _storeService.GetAllStores().Select(x => x.ToModel()).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = StoreVMs,
                        Total = StoreVMs.Count()
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
        [Route("{id:int}", Name = "GetStoreById")]
        public HttpResponseMessage GetStoreById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    var store = _storeService.GetStoreById(id);
                    if (store == null)
                    {
                        //No store found with the specified id
                        Url.Route("StoreList", null);
                        string uri = Url.Link("StoreList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = store.ToModel();
                    //languages
                    PrepareLanguagesModel(model);
                    
                    response = request.CreateResponse<StoreVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = AccessDeniedView(request);
                    //response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpGet]
        [Route("CreateStoreModel", Name = "CreateStoreModel")]
        public HttpResponseMessage CreateStoreModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    var model = new StoreVM();
                    PrepareLanguagesModel(model);
                    response = request.CreateResponse<StoreVM>(HttpStatusCode.OK, model);
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
        [Route("CreateStore", Name = "CreateStore")]
        public HttpResponseMessage CreateStore(HttpRequestMessage request, StoreVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    if (ModelState.IsValid)
                    {
                        var store = model.ToEntity();
                        //ensure we have "/" at the end
                        if (!store.Url.EndsWith("/"))
                            store.Url += "/";
                        _storeService.InsertStore(store);

                        //activity log
                        _customerActivityService.InsertActivity("AddNewStore", _localizationService.GetResource("ActivityLog.AddNewStore"), store.Id);

                        //locales
                        UpdateAttributeLocales(store, model);

                        _baseService.Commit();
                        if (continueEditing)
                        {
                            Url.Route("GetStoreById", new { id = store.Id });
                            string nUri = Url.Link("GetStoreById", new { id = store.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("StoreList", null);
                            string nuri = Url.Link("StoreList", null);
                            response.Headers.Location = new Uri(nuri);

                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        PrepareLanguagesModel(model);
                        response = request.CreateResponse<StoreVM>(HttpStatusCode.OK, model);
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
        [Route("EditStore", Name = "EditStore")]
        public HttpResponseMessage EditStore(HttpRequestMessage request, StoreVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    var store = _storeService.GetStoreById(model.Id);
                    if (store == null)
                    {
                        //No email account found with the specified id
                        Url.Route("StoreList", null);
                        string uri = Url.Link("StoreList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        store = model.ToEntity(store);
                        //ensure we have "/" at the end
                        if (!store.Url.EndsWith("/"))
                            store.Url += "/";
                        _storeService.UpdateStore(store);

                        //activity log
                        _customerActivityService.InsertActivity("EditStore", _localizationService.GetResource("ActivityLog.EditStore"), store.Id);

                        //locales
                        UpdateAttributeLocales(store, model);

                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Stores.Updated"));
                        if (continueEditing)
                        {
                            Url.Route("GetStoreById", new { id = store.Id });
                            string nUri = Url.Link("GetStoreById", new { id = store.Id });
                            response.Headers.Location = new Uri(nUri);
                        }
                        else
                        {
                            Url.Route("StoreList", null);
                            string nuri = Url.Link("StoreList", null);
                            response.Headers.Location = new Uri(nuri);
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        response = request.CreateResponse<StoreVM>(HttpStatusCode.OK, model);
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
        [Route("DeleteStore")]
        public HttpResponseMessage DeleteStore(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageStores))
                {
                    var store = _storeService.GetStoreById(id);
                    if (store == null)
                    {
                        //No email account found with the specified id
                        Url.Route("StoreList", null);
                        string uri = Url.Link("StoreList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    try
                    {
                        _storeService.DeleteStore(store);

                        //activity log
                        _customerActivityService.InsertActivity("DeleteStore", _localizationService.GetResource("ActivityLog.DeleteStore"), store.Id);

                        //when we delete a store we should also ensure that all "per store" settings will also be deleted
                        var settingsToDelete = _settingService
                            .GetAllSettings()
                            .Where(s => s.StoreId == id)
                            .ToList();
                        _settingService.DeleteSettings(settingsToDelete);
                        //when we had two stores and now have only one store, we also should delete all "per store" settings
                        var allStores = _storeService.GetAllStores();
                        if (allStores.Count == 1)
                        {
                            settingsToDelete = _settingService
                                .GetAllSettings()
                                .Where(s => s.StoreId == allStores[0].Id)
                                .ToList();
                            _settingService.DeleteSettings(settingsToDelete);
                        }

                        _baseService.Commit();

                        //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Stores.Deleted"));

                        Url.Route("StoreList", null);
                        string nUri = Url.Link("StoreList", null);
                        response.Headers.Location = new Uri(nUri);
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        Url.Route("GetStoreById", new { id = store.Id });
                        string nUri = Url.Link("GetStoreById", new { id = store.Id });
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
        #endregion
    }
}
