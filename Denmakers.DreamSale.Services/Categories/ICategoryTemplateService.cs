using Denmakers.DreamSale.Model.Catalog;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Categories
{
    public partial interface ICategoryTemplateService
    {
        /// <summary>
        /// Gets all category templates
        /// </summary>
        /// <returns>Category templates</returns>
        IList<CategoryTemplate> GetAllCategoryTemplates();

        /// <summary>
        /// Gets a category template
        /// </summary>
        /// <param name="categoryTemplateId">Category template identifier</param>
        /// <returns>Category template</returns>
        CategoryTemplate GetCategoryTemplateById(int categoryTemplateId);

        /// <summary>
        /// Inserts category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>
        void InsertCategoryTemplate(CategoryTemplate categoryTemplate);

        /// <summary>
        /// Updates the category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>

        void UpdateCategoryTemplate(CategoryTemplate categoryTemplate);
        /// <summary>
        /// Delete category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>
        void DeleteCategoryTemplate(CategoryTemplate categoryTemplate);

    }
}
