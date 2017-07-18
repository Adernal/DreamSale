using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Categories
{
    public static class CategoryExtensions
    {
        /// <summary>
        /// Sort categories for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="parentId">Parent category identifier</param>
        /// <param name="ignoreCategoriesWithoutExistingParent">A value indicating whether categories without parent category in provided category list (source) should be ignored</param>
        /// <returns>Sorted categories</returns>
        public static IList<Category> SortCategoriesForTree(this IList<Category> source, int parentId = 0, bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<Category>();

            foreach (var cat in source.Where(c => c.ParentCategoryId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.Id, true));
            }
            if (!ignoreCategoriesWithoutExistingParent && result.Count != source.Count)
            {
                //find categories without parent in provided category source and insert them into result
                foreach (var cat in source)
                    if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                        result.Add(cat);
            }
            return result;
        }

        /// <summary>
        /// Returns a ProductCategory that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>A ProductCategory that has the specified values; otherwise null</returns>
        public static ProductCategory FindProductCategory(this IList<ProductCategory> source,
            int productId, int categoryId)
        {
            foreach (var productCategory in source)
                if (productCategory.ProductId == productId && productCategory.CategoryId == categoryId)
                    return productCategory;

            return null;
        }

        //public static string GetFormattedBreadCrumb(this Category category, ICategoryService categoryService, string separator = ">>", int languageId = 0)
        //{
        //    string result = string.Empty;

        //    var breadcrumb = GetCategoryBreadCrumb(category, categoryService, null, null, true);
        //    for (int i = 0; i <= breadcrumb.Count - 1; i++)
        //    {
        //        var categoryName = breadcrumb[i].GetLocalized(x => x.Name, languageId);
        //        result = String.IsNullOrEmpty(result)
        //            ? categoryName
        //            : string.Format("{0} {1} {2}", result, separator, categoryName);
        //    }

        //    return result;
        //}

        public static string GetFormattedBreadCrumb(this Category category, IList<Category> allCategories, string separator = ">>", StringBuilder result = default(StringBuilder))
        {
            if (category.Name == "Jeans")
            {

            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(result)))
            {
                result = new StringBuilder();
                result.Append(category.Name);
            }
            var parenrCategory = allCategories.FirstOrDefault(c => c.Id == category.ParentCategoryId);
            if ((parenrCategory == null || category.ParentCategoryId == 0) 
                && (!string.IsNullOrWhiteSpace(Convert.ToString(result)) && Convert.ToString(result).Contains(category.Name))
                )
            {
                return Convert.ToString(result);
            }
            if (parenrCategory == null || category.ParentCategoryId == 0)
            {
                return category.Name;
            }
            string text = string.Format("{0} {1} {2}", parenrCategory.Name, separator, Convert.ToString(result));
            if (!text.Contains(result.ToString()))
            {
                result.Append(text);
            }
            else
            {
                result.Length = 0;
                result.Append(text);
            }
            return parenrCategory.GetFormattedBreadCrumb(allCategories, separator, result);
        }
        public static string GetFormattedBreadCrumb(this Category category, ICategoryService categoryService, string separator = ">>", StringBuilder result = default(StringBuilder))
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(result)))
            {
                result = new StringBuilder();
            }
            var parenrCategory = categoryService.GetCategoryById(category.ParentCategoryId);
            if ((parenrCategory == null || category.ParentCategoryId == 0)
                && (!string.IsNullOrWhiteSpace(Convert.ToString(result)) && Convert.ToString(result).Contains(category.Name))
                )
            {
                return Convert.ToString(result);
            }
            if (parenrCategory == null || category.ParentCategoryId == 0)
            {
                return category.Name;
            }
            string text = string.Format("{0} {1} {2}", parenrCategory.Name, separator, result.ToString());
            return parenrCategory.GetFormattedBreadCrumb(categoryService, separator, result.Append(text));
        }

    }
}
