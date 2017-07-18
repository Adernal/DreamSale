using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Model.Catalog;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Categories
{
    public partial interface ICategoryService
    {
        #region Categories
        IPagedList<Category> GetAllCategories(string categoryName = "", int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId, bool showHidden = false, bool includeAllLevels = false, bool IgnoreAcl = false, bool IgnoreStoreLimitations = false);
        IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false);

        Category GetCategoryById(int categoryId);

        string[] GetNotExistingCategories(string[] categoryNames);

        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        #endregion

        #region Product Categories
        IDictionary<int, int[]> GetProductCategoryIds(int[] productIds);
        IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false);
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, int storeId, bool showHidden = false);
        ProductCategory GetProductCategoryById(int productCategoryId);
        void InsertProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(ProductCategory productCategory);
        #endregion
    }
}
