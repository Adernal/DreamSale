using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Security;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Categories
{
    public partial class CategoryService : ICategoryService
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IAclService _aclService;
        private readonly ISettingService _settingService;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor
        public CategoryService(IRepository<Category> categoryRepository, IRepository<ProductCategory> productCategoryRepository, IRepository<Product> productRepository, IRepository<StoreMapping> storeMappingRepository, IStoreMappingService _storeMappingService, IRepository<AclRecord> aclRepository, IUnitOfWork unitOfWork, IWorkContext workContext, IStoreContext storeContext, ISettingService settingService, IAclService aclService)
        {
            this._unitOfWork = unitOfWork;
            this._categoryRepository = categoryRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._productRepository = productRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._aclRepository = aclRepository;
            this._storeMappingService = _storeMappingService;
            this._settingService = settingService;
            this._aclService = aclService;
            this._commonSettings = _settingService.LoadSetting<CommonSettings>();
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
            this._workContext = workContext;
            this._storeContext = storeContext;
        }
        #endregion

        #region Categories
        public IPagedList<Category> GetAllCategories(string categoryName = "", int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);

            if ((storeId > 0 && !_catalogSettings.IgnoreStoreLimitations) || (!showHidden && !_catalogSettings.IgnoreAcl))
            {
                if (!showHidden && !_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.GetCustomerRoleIds();
                    query = from c in query
                            join acl in _aclRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select c;
                }
                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || storeId == sm.StoreId
                            select c;
                }

                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            }

            var unsortedCategories = query.ToList();

            //sort categories
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<Category>(sortedCategories, pageIndex, pageSize);
        }
        public IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId, bool showHidden = false, bool includeAllLevels = false, bool IgnoreAcl = false, bool IgnoreStoreLimitations = false)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => c.ParentCategoryId == parentCategoryId);
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Id);

            if (!showHidden && (!IgnoreAcl || !IgnoreStoreLimitations))
            {
                if (!IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.GetCustomerRoleIds();
                    query = from c in query
                            join acl in _aclRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select c;
                }
                if (!IgnoreStoreLimitations)
                {
                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || currentStoreId == sm.StoreId
                            select c;
                }
                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            }

            var categories = query.ToList();
            if (includeAllLevels)
            {
                var childCategories = new List<Category>();
                //add child levels
                foreach (var category in categories)
                {
                    childCategories.AddRange(GetAllCategoriesByParentCategoryId(category.Id, showHidden, includeAllLevels));
                }
                categories.AddRange(childCategories);
            }
            return categories;
        }

        public IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            var query = from c in _categoryRepository.Table
                        orderby c.DisplayOrder, c.Id
                        where c.Published &&
                        !c.Deleted &&
                        c.ShowOnHomePage
                        select c;

            var categories = query.ToList();
            if (!showHidden)
            {
                categories = categories
                    .Where(c => _aclService.Authorize(c) && _storeMappingService.Authorize(c))
                    .ToList();
            }

            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;
            return _categoryRepository.GetById(categoryId);
        }

        public string[] GetNotExistingCategories(string[] categoryNames)
        {
            if (categoryNames == null)
                throw new ArgumentNullException("categoryNames");

            var query = _categoryRepository.Table;
            var queryFilter = categoryNames.Distinct().ToArray();
            var filter = query.Select(c => c.Name).Where(c => queryFilter.Contains(c)).ToList();

            return queryFilter.Except(filter).ToArray();
        }

        public void InsertCategory(Category category)
        {
            if(category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);
            _unitOfWork.Commit();
        }
        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetCategoryById(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }

            _categoryRepository.Update(category);
            _unitOfWork.Commit();
        }
        public void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Deleted = true;
            UpdateCategory(category);

            //reset a "Parent category" property of all child subcategories
            var subcategories = GetAllCategoriesByParentCategoryId(category.Id, true);
            foreach (var subcategory in subcategories)
            {
                subcategory.ParentCategoryId = 0;
                UpdateCategory(subcategory);
            }
            _unitOfWork.Commit();
        }
        #endregion

        #region Product Categories
        public IDictionary<int, int[]> GetProductCategoryIds(int[] productIds)
        {
            throw new NotImplementedException();
        }
        public IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<ProductCategory>(new List<ProductCategory>(), pageIndex, pageSize);
            var query = from pc in _productCategoryRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.CategoryId == categoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder, pc.Id
                        select pc;

            if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
            {
                if (!_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.GetCustomerRoleIds();
                    query = from pc in query
                            join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                            join acl in _aclRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select pc;
                }
                if (!_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    query = from pc in query
                            join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                            join sm in _storeMappingRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || currentStoreId == sm.StoreId
                            select pc;
                }
                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(pc => pc.DisplayOrder).ThenBy(pc => pc.Id);
            }

            var productCategories = new PagedList<ProductCategory>(query, pageIndex, pageSize);
            return productCategories;
        }
        public IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false)
        {
            return GetProductCategoriesByProductId(productId, 1, showHidden);
        }
        public IList<ProductCategory> GetProductCategoriesByProductId(int productId, int storeId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductCategory>();
            var query = from pc in _productCategoryRepository.Table
                        join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                        where pc.ProductId == productId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder, pc.Id
                        select pc;

            var allProductCategories = query.ToList();
            var result = new List<ProductCategory>();
            if (!showHidden)
            {
                foreach (var pc in allProductCategories)
                {
                    //ACL (access control list) and store mapping
                    var category = pc.Category;
                    if (_aclService.Authorize(category) && _storeMappingService.Authorize(category, storeId))
                        result.Add(pc);
                }
            }
            else
            {
                //no filtering
                result.AddRange(allProductCategories);
            }
            return result;
        }
        public ProductCategory GetProductCategoryById(int productCategoryId)
        {
            if (productCategoryId == 0)
                return null;

            return _productCategoryRepository.GetById(productCategoryId);
        }
        public void InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Insert(productCategory);
            _unitOfWork.Commit();
        }
        public void UpdateProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Update(productCategory);
            _unitOfWork.Commit();
        }
        public void DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Delete(productCategory);
            _unitOfWork.Commit();
        }
        #endregion
    }
}
