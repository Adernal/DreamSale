using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Denmakers.DreamSale.Services.Products
{
    public partial class ProductTagService : IProductTagService
    {
        #region Fields

        private readonly IRepository<ProductTag> _productTagRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly ISettingService _settingService;
        private readonly IProductService _productService;
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public ProductTagService(IRepository<ProductTag> productTagRepository,
            IRepository<StoreMapping> storeMappingRepository,
            ISettingService settingService,
            IProductService productService,
            IDbContext dbContext,
            IUnitOfWork unitOfWork
            //ICacheManager cacheManager,
            )
        {
            this._productTagRepository = productTagRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._productService = productService;
            this._settingService = settingService;
            this._dbContext = dbContext;
            this._unitOfWork = unitOfWork;
            this._commonSettings = _settingService.LoadSetting<CommonSettings>();
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
            //this._cacheManager = cacheManager;
        }

        #endregion

        #region Nested classes

        private class ProductTagWithCount
        {
            public int ProductTagId { get; set; }
            public int ProductCount { get; set; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get product count for each of existing product tag
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Dictionary of "product tag ID : product count"</returns>
        private Dictionary<int, int> GetProductCount(int storeId)
        {
            if (_commonSettings.UseStoredProceduresIfSupported)
            {
                //stored procedures are enabled and supported by the database. 
                //It's much faster than the LINQ implementation below 

                #region Use stored procedure

                //prepare parameters
                var pStoreId = new SqlParameter();
                pStoreId.ParameterName = "StoreId";
                pStoreId.Value = storeId;
                pStoreId.DbType = DbType.Int32;


                //invoke stored procedure
                var result = _dbContext.SqlQuery<ProductTagWithCount>(
                    "Exec ProductTagCountLoadAll @StoreId",
                    pStoreId);

                var dictionary = new Dictionary<int, int>();
                foreach (var item in result)
                    dictionary.Add(item.ProductTagId, item.ProductCount);
                return dictionary;

                #endregion
            }
            else
            {
                //stored procedures aren't supported. Use LINQ
                #region Search products
                var query = _productTagRepository.Table.Select(pt => new
                {
                    Id = pt.Id,
                    ProductCount = (storeId == 0 || _catalogSettings.IgnoreStoreLimitations) ?
                        pt.Products.Count(p => !p.Deleted && p.Published)
                        : (from p in pt.Products
                           join sm in _storeMappingRepository.Table
                           on new { p1 = p.Id, p2 = "Product" } equals new { p1 = sm.EntityId, p2 = sm.EntityName } into p_sm
                           from sm in p_sm.DefaultIfEmpty()
                           where (!p.LimitedToStores || storeId == sm.StoreId) && !p.Deleted && p.Published
                           select p).Count()
                });
                var dictionary = new Dictionary<int, int>();
                foreach (var item in query)
                    dictionary.Add(item.Id, item.ProductCount);
                return dictionary;

                #endregion

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a product tag
        /// </summary>
        /// <param name="productTag">Product tag</param>
        public virtual void DeleteProductTag(ProductTag productTag)
        {
            if (productTag == null)
                throw new ArgumentNullException("productTag");

            _productTagRepository.Delete(productTag);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all product tags
        /// </summary>
        /// <returns>Product tags</returns>
        public virtual IPagedList<ProductTag> GetAllProductTags(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _productTagRepository.Table;
            var productTags = new PagedList<ProductTag>(query, pageIndex, pageSize);
            return productTags;
        }

        /// <summary>
        /// Gets product tag
        /// </summary>
        /// <param name="productTagId">Product tag identifier</param>
        /// <returns>Product tag</returns>
        public virtual ProductTag GetProductTagById(int productTagId)
        {
            if (productTagId == 0)
                return null;

            return _productTagRepository.GetById(productTagId);
        }

        /// <summary>
        /// Gets product tag by name
        /// </summary>
        /// <param name="name">Product tag name</param>
        /// <returns>Product tag</returns>
        public virtual ProductTag GetProductTagByName(string name)
        {
            var query = from pt in _productTagRepository.Table
                        where pt.Name == name
                        select pt;

            var productTag = query.FirstOrDefault();
            return productTag;
        }

        /// <summary>
        /// Inserts a product tag
        /// </summary>
        /// <param name="productTag">Product tag</param>
        public virtual void InsertProductTag(ProductTag productTag)
        {
            if (productTag == null)
                throw new ArgumentNullException("productTag");

            _productTagRepository.Insert(productTag);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the product tag
        /// </summary>
        /// <param name="productTag">Product tag</param>
        public virtual void UpdateProductTag(ProductTag productTag)
        {
            if (productTag == null)
                throw new ArgumentNullException("productTag");

            _productTagRepository.Update(productTag);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Get number of products
        /// </summary>
        /// <param name="productTagId">Product tag identifier</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Number of products</returns>
        public virtual int GetProductCount(int productTagId, int storeId)
        {
            var dictionary = GetProductCount(storeId);
            if (dictionary.ContainsKey(productTagId))
                return dictionary[productTagId];

            return 0;
        }

        /// <summary>
        /// Update product tags
        /// </summary>
        /// <param name="product">Product for update</param>
        /// <param name="productTags">Product tags</param>
        public virtual void UpdateProductTags(Product product, string[] productTags)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //product tags
            var existingProductTags = product.ProductTags.ToList();
            var productTagsToRemove = new List<ProductTag>();
            foreach (var existingProductTag in existingProductTags)
            {
                var found = false;
                foreach (var newProductTag in productTags)
                {
                    if (existingProductTag.Name.Equals(newProductTag, StringComparison.InvariantCultureIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    productTagsToRemove.Add(existingProductTag);
                }
            }
            foreach (var productTag in productTagsToRemove)
            {
                product.ProductTags.Remove(productTag);
                _productService.UpdateProduct(product);
            }
            foreach (var productTagName in productTags)
            {
                ProductTag productTag;
                var productTag2 = GetProductTagByName(productTagName);
                if (productTag2 == null)
                {
                    //add new product tag
                    productTag = new ProductTag
                    {
                        Name = productTagName
                    };
                    InsertProductTag(productTag);
                }
                else
                {
                    productTag = productTag2;
                }
                if (!product.ProductTagExists(productTag.Id))
                {
                    product.ProductTags.Add(productTag);
                    _productService.UpdateProduct(product);
                }
            }
        }
        #endregion
    }
}
