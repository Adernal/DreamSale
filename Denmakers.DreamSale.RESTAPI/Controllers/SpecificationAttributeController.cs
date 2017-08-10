using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/SpecificationAttribute")]
    [Infrastructure.Securities.AdminAuthorize]
    public class SpecificationAttributeController : ApiControllerBase
    {
        #region Fields
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;

        #endregion Fields

        #region Ctor
        public SpecificationAttributeController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ISpecificationAttributeService specificationAttributeService,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            ILocalizationService localizationService,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService)
            : base(baseService, logger, webHelper)
        {
            this._specificationAttributeService = specificationAttributeService;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._localizationService = localizationService;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
        }
        #endregion

        #region Specification attributes
        [HttpGet]
        [Route("{pageIndex:int=0}/{pageSize:int=2147483647}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item(s) found");
                var specificationAttributes = _specificationAttributeService.GetSpecificationAttributes(pageIndex, pageSize);
                if (specificationAttributes != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = specificationAttributes.Select(x => x.ToModel()),
                        Total = specificationAttributes.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{Id}", Name = "GetSpecificationAttributeById")]
        public HttpResponseMessage GettById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                var specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(id);
                if (specificationAttribute != null)
                {
                    var model = specificationAttribute.ToModel();

                    response = request.CreateResponse<SpecificationAttributeVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }


        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, SpecificationAttributeVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var specificationAttribute = model.ToEntity();
                    _specificationAttributeService.InsertSpecificationAttribute(specificationAttribute);

                    //activity log
                    _customerActivityService.InsertActivity("AddNewSpecAttribute", _localizationService.GetResource("ActivityLog.AddNewSpecAttribute"), specificationAttribute.Name);

                    _baseService.Commit();
                    response = request.CreateResponse<SpecificationAttribute>(HttpStatusCode.Created, specificationAttribute);
                    if (continueEditing)
                    {
                        // Generate a link to the update item and set the Location header in the response.
                        string uri = Url.Link("GetSpecificationAttributeById", new { id = specificationAttribute.Id });
                        response.Headers.Location = new Uri(uri);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, SpecificationAttributeVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(model.Id);
                    if (specificationAttribute != null)
                    {
                        _specificationAttributeService.UpdateSpecificationAttribute(specificationAttribute);
                        
                        //activity log
                        _customerActivityService.InsertActivity("EditSpecAttribute", _localizationService.GetResource("ActivityLog.EditSpecAttribute"), specificationAttribute.Name);

                        _baseService.Commit();
                        response = request.CreateResponse<SpecificationAttribute>(HttpStatusCode.OK, specificationAttribute);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetSpecificationAttributeById", new { id = specificationAttribute.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(id);
                    if (specificationAttribute != null)
                    {
                        _specificationAttributeService.DeleteSpecificationAttribute(specificationAttribute);
                        //activity log
                        _customerActivityService.InsertActivity("DeleteSpecAttribute", _localizationService.GetResource("ActivityLog.DeleteSpecAttribute"), specificationAttribute.Name);

                        _baseService.Commit();
                        response = request.CreateResponse<SpecificationAttribute>(HttpStatusCode.OK, specificationAttribute);
                    }
                }
                return response;
            });
        }
        #endregion

        #region Specification attribute options
        [HttpGet]
        [Route("{specificationAttributeId:int}/options/getType")]
        public HttpResponseMessage GetOptionListBySAId(HttpRequestMessage request, int specificationAttributeId, string getType = "list")
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid specification attribute id.");
                if (getType.Equals("create", StringComparison.InvariantCultureIgnoreCase))
                {
                    var specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(specificationAttributeId);
                    if (specificationAttribute != null)
                    {
                        var model = new SpecificationAttributeOptionVM
                        {
                            SpecificationAttributeId = specificationAttributeId
                        };
                        response = request.CreateResponse<SpecificationAttributeOptionVM>(HttpStatusCode.OK, model);
                    }
                }
                else if (getType.Equals("list", StringComparison.InvariantCultureIgnoreCase))
                {
                    var options = _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(specificationAttributeId);
                    if (options != null)
                    {
                        var gridModel = new DataSourceResult
                        {
                            Data = options.Select(x =>
                            {
                                var model = x.ToModel();
                                //in order to save performance to do not check whether a product is deleted, etc
                                model.NumberOfAssociatedProducts = _specificationAttributeService.GetProductSpecificationAttributeCount(0, x.Id);
                                return model;
                            }),
                            Total = options.Count()
                        };

                        response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                    }
                }

                return response;
            });
        }

        [HttpGet]
        [Route("option/{id:int}")]
        public HttpResponseMessage GetOptionById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid option id.");
                var sao = _specificationAttributeService.GetSpecificationAttributeOptionById(id);
                if (sao != null)
                {
                    var model = sao.ToModel();
                    //"Color" value
                    model.EnableColorSquaresRgb = !String.IsNullOrEmpty(sao.ColorSquaresRgb);
                    response = request.CreateResponse<SpecificationAttributeOptionVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("CreateOption")]
        public HttpResponseMessage CreateOption(HttpRequestMessage request, SpecificationAttributeOptionVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid specification attribute id.");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var specificationAttribute = _specificationAttributeService.GetSpecificationAttributeById(model.SpecificationAttributeId);
                    if (specificationAttribute != null)
                    {
                        var sao = model.ToEntity();
                        //clear "Color" values if it's disabled
                        if (!model.EnableColorSquaresRgb)
                            sao.ColorSquaresRgb = null;

                        _specificationAttributeService.InsertSpecificationAttributeOption(sao);

                        _baseService.Commit();
                        response = request.CreateResponse<SpecificationAttributeOption>(HttpStatusCode.Created, sao);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("EditOption")]
        public virtual HttpResponseMessage EditOption(HttpRequestMessage request, SpecificationAttributeOptionVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No specification attribute option found with the specified id");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var sao = _specificationAttributeService.GetSpecificationAttributeOptionById(model.Id);

                    if (sao != null)
                    {
                        sao = model.ToEntity(sao);
                        //clear "Color" values if it's disabled
                        if (!model.EnableColorSquaresRgb)
                            sao.ColorSquaresRgb = null;

                        _specificationAttributeService.UpdateSpecificationAttributeOption(sao);

                        _baseService.Commit();
                        response = request.CreateResponse<SpecificationAttributeOptionVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }
        
        [HttpPost]
        [Route("DeleteOption/{id:int}")]
        public virtual HttpResponseMessage DeleteOption(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No specification attribute option found with the specified id");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var sao = _specificationAttributeService.GetSpecificationAttributeOptionById(id);

                    if (sao != null)
                    {
                        _specificationAttributeService.DeleteSpecificationAttributeOption(sao);

                        _baseService.Commit();
                        response = request.CreateResponse<SpecificationAttributeOption>(HttpStatusCode.OK, sao);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{attributeId:int}/SpecificationAttributeOptions")]
        public HttpResponseMessage GetOptionsByAttributeId(HttpRequestMessage request, int attributeId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid attribute id.");
                var options = _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(Convert.ToInt32(attributeId));
                if (options != null)
                {
                    var result = (from o in options
                                  select new { id = o.Id, name = o.Name }).ToList();
                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
        #endregion
    }
}
