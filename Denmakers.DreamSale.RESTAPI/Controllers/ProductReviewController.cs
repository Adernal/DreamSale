using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Web.Http;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Model.Logging;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/ProductReviews")]
    public class ProductReviewController : ApiControllerBase
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreService _storeService;
        private readonly ICustomerActivityService _customerActivityService;

        #endregion Fields

        #region Constructors
        public ProductReviewController(IRepository<Log> log, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper,
            IProductService productService,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IStoreService storeService,
            ICustomerActivityService customerActivityService)
            : base(log, unitOfWork, workContext, webHelper)
        {
            this._productService = productService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._storeService = storeService;
            this._customerActivityService = customerActivityService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareProductReviewVM(ProductReviewVM model, ProductReview productReview, bool excludeProperties, bool formatReviewAndReplyText)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (productReview == null)
                throw new ArgumentNullException("productReview");

            model.Id = productReview.Id;
            model.StoreName = productReview.Store.Name;
            model.ProductId = productReview.ProductId;
            model.ProductName = productReview.Product.Name;
            model.CustomerId = productReview.CustomerId;
            var customer = productReview.Customer;
            model.CustomerInfo = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
            model.Rating = productReview.Rating;
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(productReview.CreatedOnUtc, DateTimeKind.Utc);
            if (!excludeProperties)
            {
                model.Title = productReview.Title;
                if (formatReviewAndReplyText)
                {
                    model.ReviewText = HtmlHelper.FormatText(productReview.ReviewText, false, true, false, false, false, false);
                    model.ReplyText = HtmlHelper.FormatText(productReview.ReplyText, false, true, false, false, false, false);
                }
                else
                {
                    model.ReviewText = productReview.ReviewText;
                    model.ReplyText = productReview.ReplyText;
                }
                model.IsApproved = productReview.IsApproved;
            }

            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

        }

        #endregion

        #region Methods
        [HttpGet]
        [Route("DefaultPageLoad")]
        public HttpResponseMessage DefaultPageLoad(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");

                var model = new ProductReviewListVM();
                //a vendor should have access only to his products
                model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

                model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                var stores = _storeService.GetAllStores().Select(st => new System.Web.Mvc.SelectListItem() { Text = st.Name, Value = st.Id.ToString() });
                foreach (var selectListItem in stores)
                    model.AvailableStores.Add(selectListItem);

                //"approved" property
                //0 - all
                //1 - approved only
                //2 - disapproved only
                model.AvailableApprovedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.ProductReviews.List.SearchApproved.All"), Value = "0" });
                model.AvailableApprovedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.ProductReviews.List.SearchApproved.ApprovedOnly"), Value = "1" });
                model.AvailableApprovedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.ProductReviews.List.SearchApproved.DisapprovedOnly"), Value = "2" });

                response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpGet]
        [Route("", Name = "GetAllProductReviews")]
        public HttpResponseMessage GetAllProductReviews(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var productReviews = _productService.GetAllProductReviews(0, (bool?)null);
                if (productReviews != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = productReviews.Select(x =>
                        {
                            var m = new ProductReviewVM();
                            PrepareProductReviewVM(m, x, false, true);
                            return m;
                        }),
                        Total = productReviews.TotalCount
                    };

                    response = request.CreateResponse< DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("Search/{approved:bool?}/{reviewText=null}/{storeId:int=0}/{productId:int=0}/{vendorId:int=0}/{createdOnFromValue:dateTime?}/{createdToFromValue:dateTime?}/{pageIndex:int=0}/{pageSize:int=2147483647}")]
        public HttpResponseMessage GetBySearchCriteria(HttpRequestMessage request, bool? approved, string reviewText, int storeId, int productId, int vendorId = 0, DateTime? createdOnFromValue = null, DateTime? createdToFromValue = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");

                var productReviews = _productService.GetAllProductReviews(0, approved, createdOnFromValue, createdToFromValue, reviewText, storeId, productId, vendorId, pageIndex, pageSize);
                if (productReviews != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = productReviews.Select(x =>
                        {
                            var m = new ProductReviewVM();
                            PrepareProductReviewVM(m, x, false, true);
                            return m;
                        }),
                        Total = productReviews.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductReviewById")]
        public HttpResponseMessage GettById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");

                var productReview = _productService.GetProductReviewById(id);
                if (productReview != null)
                {
                    if (_workContext.CurrentVendor != null && productReview.Product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "A vendor should only edit his products reviews");
                    }
                    else
                    {
                        var model = new ProductReviewVM();
                        PrepareProductReviewVM(model, productReview, false, false);

                        response = request.CreateResponse<ProductReviewVM>(HttpStatusCode.OK, model);
                        //string uri = Url.Link("GetAll", null);
                        //response.Headers.Location = new Uri(uri);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("ProductSearchAutoComplete/{term}")]
        public HttpResponseMessage ProductSearchAutoComplete(HttpRequestMessage request, string term)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var productReviews = _productService.GetAllProductReviews(0, (bool?)null);
                if (productReviews != null)
                {
                    response = request.CreateResponse<List<ProductReview>>(HttpStatusCode.OK, productReviews.ToList());
                }
                const int searchTermMinimumLength = 3;
                if (String.IsNullOrWhiteSpace(term) || term.Length < searchTermMinimumLength)
                    return response;

                //a vendor should have access only to his products
                var vendorId = 0;
                if (_workContext.CurrentVendor != null)
                {
                    vendorId = _workContext.CurrentVendor.Id;
                }

                //products
                const int productNumber = 15;
                var products = _productService.SearchProducts(
                    keywords: term,
                    vendorId: vendorId,
                    pageSize: productNumber,
                    showHidden: true);

                var result = (from p in products
                              select new
                              {
                                  label = p.Name,
                                  productid = p.Id
                              })
                    .ToList();
                if (result != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductReviewVM prodReviewVM, bool continueEditing)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                var productReview = _productService.GetProductReviewById(prodReviewVM.Id);
                if (!ModelState.IsValid)
                {
                    var model = new ProductReviewVM();
                    PrepareProductReviewVM(model, productReview, true, false);
                    response = request.CreateResponse<ProductReviewVM>(HttpStatusCode.BadRequest, model);
                }
                else if (productReview != null)
                {
                    if (_workContext.CurrentVendor != null && productReview.Product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "A vendor should have access only to his products");
                    }
                    else
                    {
                        var isLoggedInAsVendor = _workContext.CurrentVendor != null;

                        var previousIsApproved = productReview.IsApproved;
                        //vendor can edit "Reply text" only
                        if (!isLoggedInAsVendor)
                        {
                            productReview.Title = prodReviewVM.Title;
                            productReview.ReviewText = prodReviewVM.ReviewText;
                            productReview.IsApproved = prodReviewVM.IsApproved;
                        }

                        productReview.ReplyText = prodReviewVM.ReplyText;

                        _productService.UpdateProduct(productReview.Product);

                        //activity log
                        _customerActivityService.InsertActivity("EditProductReview", _localizationService.GetResource("ActivityLog.EditProductReview"), productReview.Id);

                        //vendor can edit "Reply text" only
                        if (!isLoggedInAsVendor)
                        {
                            //update product totals
                            _productService.UpdateProductReviewTotals(productReview.Product);
                        }
                        response = request.CreateResponse<ProductReview>(HttpStatusCode.OK, productReview);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetProductReviewById", new { id = productReview.Id });
                            response.Headers.Location = new Uri(uri); 
                        }
                        //else
                        //{
                        //    string uri = Url.Link("GetAll",null);
                        //    response.Headers.Location = new Uri(uri);
                            
                        //}
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, ProductReview prodReview)
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
                    var productReview = _productService.GetProductReviewById(prodReview.Id);
                    if (productReview != null)
                    {
                        //a vendor does not have access to delete functionality
                        if (_workContext.CurrentVendor != null)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "A vendor can not delete his products reviews");
                        }
                        else
                        {
                            _productService.DeleteProductReview(productReview);

                            //activity log
                            _customerActivityService.InsertActivity("DeleteProductReview", _localizationService.GetResource("ActivityLog.DeleteProductReview"), productReview.Id);

                            //update product totals
                            _productService.UpdateProductReviewTotals(productReview.Product);

                            response = request.CreateResponse<ProductReview>(HttpStatusCode.OK, productReview);
                        }
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("DeleteSelected")]
        public HttpResponseMessage DeleteSelected(HttpRequestMessage request, ICollection<int> selectedIds)
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
                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "A vendor can not delete his products reviews");

                    else if (selectedIds != null)
                    {
                        var productReviews = _productService.GetProducReviewsByIds(selectedIds.ToArray());
                        var products = _productService.GetProductsByIds(productReviews.Select(p => p.ProductId).Distinct().ToArray());

                        _productService.DeleteProductReviews(productReviews);

                        //update product totals
                        foreach (var product in products)
                        {
                            _productService.UpdateProductReviewTotals(product);
                        }
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("ApproveSelected")]
        public HttpResponseMessage ApproveSelected(HttpRequestMessage request, ICollection<int> selectedIds)
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
                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "A vendor can not approve his products reviews");

                    else if (selectedIds != null)
                    {
                        //filter not approved reviews
                        var productReviews = _productService.GetProducReviewsByIds(selectedIds.ToArray()).Where(review => !review.IsApproved);
                        foreach (var productReview in productReviews)
                        {
                            productReview.IsApproved = true;
                            _productService.UpdateProduct(productReview.Product);

                            //update product totals
                            _productService.UpdateProductReviewTotals(productReview.Product);
                        }
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("DisapproveSelected")]
        public HttpResponseMessage DisapproveSelected(HttpRequestMessage request, ICollection<int> selectedIds)
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
                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "A vendor can not disapprove his products reviews");

                    else if (selectedIds != null)
                    {
                        //filter not approved reviews
                        var productReviews = _productService.GetProducReviewsByIds(selectedIds.ToArray()).Where(review => review.IsApproved);
                        foreach (var productReview in productReviews)
                        {
                            productReview.IsApproved = false;
                            _productService.UpdateProduct(productReview.Product);

                            //update product totals
                            _productService.UpdateProductReviewTotals(productReview.Product);
                        }
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        #endregion
    }
}
