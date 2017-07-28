using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
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
    [RoutePrefix("api/ProductAttribute")]
    public class ProductAttributeController : ApiControllerBase
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;

        #endregion Fields

        #region Ctor
        public ProductAttributeController(IRepository<Log> log, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper,
            IProductService productService,
            IProductAttributeService productAttributeService,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            ILocalizationService localizationService,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService)
            : base(log, unitOfWork, workContext, webHelper)
        {
            this._productService = productService;
            this._productAttributeService = productAttributeService;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._localizationService = localizationService;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
        }
        #endregion

        #region Product Attributes : CRUD
        [HttpGet]
        [Route("{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "PAList")]
        public HttpResponseMessage Get(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var prodAttributes = _productAttributeService.GetAllProductAttributes(pageIndex, pageSize);
                if (prodAttributes != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = prodAttributes.Select(x => x.ToModel()),
                        Total = prodAttributes.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{Id}", Name = "GetProductAttributeById")]
        public HttpResponseMessage GettById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                var productAttribute = _productAttributeService.GetProductAttributeById(id);
                if (productAttribute != null)
                {
                    var model = productAttribute.ToModel();
                    
                    response = request.CreateResponse<ProductAttributeVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }


        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductAttributeVM model, bool continueEditing = false)
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
                    var productAttribute = model.ToEntity();
                    _productAttributeService.InsertProductAttribute(productAttribute);

                    //activity log
                    _customerActivityService.InsertActivity("AddNewProductAttribute", _localizationService.GetResource("ActivityLog.AddNewProductAttribute"), productAttribute.Name);

                    _unitOfWork.Commit();
                    response = request.CreateResponse<ProductAttribute>(HttpStatusCode.Created, productAttribute);
                    if (continueEditing)
                    {
                        // Generate a link to the update item and set the Location header in the response.
                        string uri = Url.Link("GetProductAttributeById", new { id = productAttribute.Id });
                        response.Headers.Location = new Uri(uri);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductAttributeVM model, bool continueEditing = false)
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
                    var productAttribute = _productAttributeService.GetProductAttributeById(model.Id);
                    if (productAttribute != null)
                    {
                        productAttribute = model.ToEntity(productAttribute);
                        _productAttributeService.UpdateProductAttribute(productAttribute);
                        //activity log
                        _customerActivityService.InsertActivity("EditProductAttribute", _localizationService.GetResource("ActivityLog.EditProductAttribute"), productAttribute.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<ProductAttribute>(HttpStatusCode.OK, productAttribute);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetProductAttributeById", new { id = productAttribute.Id });
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
                    var productAttribute = _productAttributeService.GetProductAttributeById(id);
                    if (productAttribute != null)
                    {
                        _productAttributeService.DeleteProductAttribute(productAttribute);
                        //activity log
                        _customerActivityService.InsertActivity("DeleteProductAttribute", _localizationService.GetResource("ActivityLog.DeleteProductAttribute"), productAttribute.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<ProductAttribute>(HttpStatusCode.OK, productAttribute);
                    }
                }
                return response;
            });
        }
        #endregion

        #region Used by products

        [HttpGet]
        [Route("{productAttributeId:int}/products/{pageIndex:int=0}/{pageSize:int=2147483647}")]
        public HttpResponseMessage UsedByProducts(HttpRequestMessage request, int productAttributeId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var products = _productService.GetProductsByProductAtributeId(productAttributeId, pageIndex, pageSize);
                if (products.Any())
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = products.Select(x =>
                        {
                            return new ProductAttributeVM.UsedByProductVM
                            {
                                Id = x.Id,
                                ProductName = x.Name,
                                Published = x.Published
                            };
                        }),
                        Total = products.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }

                return response;
            });
        }

        #endregion

        #region Predefined values

        [HttpGet]
        [Route("{productAttributeId:int}/PredefinedProductAttributeValue/{getType}")]
        public HttpResponseMessage PredefinedProductAttributeValueList(HttpRequestMessage request, int productAttributeId, string getType = "list")
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (getType.Equals("create", StringComparison.InvariantCultureIgnoreCase))
                {
                    var productAttribute = _productAttributeService.GetProductAttributeById(productAttributeId);
                    if (productAttribute != null)
                    {
                        var model = new PredefinedProductAttributeValueVM();
                        model.ProductAttributeId = productAttributeId;

                        response = request.CreateResponse<PredefinedProductAttributeValueVM>(HttpStatusCode.OK, model);
                    }
                }
                else if (getType.Equals("list", StringComparison.InvariantCultureIgnoreCase))
                {
                    var values = _productAttributeService.GetPredefinedProductAttributeValues(productAttributeId);
                    if (values != null && values.Any())
                    {
                        var gridModel = new DataSourceResult
                        {
                            Data = values.Select(x =>
                            {
                                return new PredefinedProductAttributeValueVM
                                {
                                    Id = x.Id,
                                    ProductAttributeId = x.ProductAttributeId,
                                    Name = x.Name,
                                    PriceAdjustment = x.PriceAdjustment,
                                    PriceAdjustmentStr = x.PriceAdjustment.ToString("G29"),
                                    WeightAdjustment = x.WeightAdjustment,
                                    WeightAdjustmentStr = x.WeightAdjustment.ToString("G29"),
                                    Cost = x.Cost,
                                    IsPreSelected = x.IsPreSelected,
                                    DisplayOrder = x.DisplayOrder
                                };
                            }),
                            Total = values.Count()
                        };

                        response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                    }
                }

                return response;
            });
        }

        [HttpGet]
        [Route("{PredefinedProductAttributeValue}/{id:int}")]
        public HttpResponseMessage PredefinedProductAttributeValueEditPopup(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var ppav = _productAttributeService.GetPredefinedProductAttributeValueById(id);
                if (ppav != null)
                {
                    var model = new PredefinedProductAttributeValueVM
                    {
                        ProductAttributeId = ppav.ProductAttributeId,
                        Name = ppav.Name,
                        PriceAdjustment = ppav.PriceAdjustment,
                        WeightAdjustment = ppav.WeightAdjustment,
                        Cost = ppav.Cost,
                        IsPreSelected = ppav.IsPreSelected,
                        DisplayOrder = ppav.DisplayOrder
                    };

                    response = request.CreateResponse<PredefinedProductAttributeValueVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("CreatePredefinedProductAttributeValue")]
        public HttpResponseMessage PredefinedProductAttributeValueCreatePopup(HttpRequestMessage request, PredefinedProductAttributeValueVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid product attribute id.");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productAttribute = _productAttributeService.GetProductAttributeById(model.ProductAttributeId);
                    if (productAttribute != null)
                    {
                        var ppav = new PredefinedProductAttributeValue
                        {
                            ProductAttributeId = model.ProductAttributeId,
                            Name = model.Name,
                            PriceAdjustment = model.PriceAdjustment,
                            WeightAdjustment = model.WeightAdjustment,
                            Cost = model.Cost,
                            IsPreSelected = model.IsPreSelected,
                            DisplayOrder = model.DisplayOrder
                        };

                        _productAttributeService.InsertPredefinedProductAttributeValue(ppav);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PredefinedProductAttributeValue>(HttpStatusCode.Created, ppav);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("EditPredefinedProductAttributeValue")]
        public virtual HttpResponseMessage PredefinedProductAttributeValueEditPopup(HttpRequestMessage request, PredefinedProductAttributeValueVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No product attribute value found with the specified id");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var existingPpav = _productAttributeService.GetPredefinedProductAttributeValueById(model.Id);
                    
                    if (existingPpav != null)
                    {
                        existingPpav.Name = model.Name;
                        existingPpav.PriceAdjustment = model.PriceAdjustment;
                        existingPpav.WeightAdjustment = model.WeightAdjustment;
                        existingPpav.Cost = model.Cost;
                        existingPpav.IsPreSelected = model.IsPreSelected;
                        existingPpav.DisplayOrder = model.DisplayOrder;
                        _productAttributeService.UpdatePredefinedProductAttributeValue(existingPpav);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PredefinedProductAttributeValueVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("DeletePredefinedProductAttributeValue/{id:int}")]
        public virtual HttpResponseMessage PredefinedProductAttributeValueDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No product attribute value found with the specified id");
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var ppav = _productAttributeService.GetPredefinedProductAttributeValueById(id);

                    if (ppav != null)
                    {
                        _productAttributeService.DeletePredefinedProductAttributeValue(ppav);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PredefinedProductAttributeValue>(HttpStatusCode.OK, ppav);
                    }
                }
                return response;
            });
        }
        #endregion
    }
}
