using Denmakers.DreamSale.Model.Tax;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Tax
{
    public partial interface ITaxCategoryService
    {
        /// <summary>
        /// Gets all tax categories
        /// </summary>
        /// <returns>Tax categories</returns>
        IList<TaxCategory> GetAllTaxCategories();

        /// <summary>
        /// Gets a tax category
        /// </summary>
        /// <param name="taxCategoryId">Tax category identifier</param>
        /// <returns>Tax category</returns>
        TaxCategory GetTaxCategoryById(int taxCategoryId);

        /// <summary>
        /// Inserts a tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        void InsertTaxCategory(TaxCategory taxCategory);

        /// <summary>
        /// Updates the tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        void UpdateTaxCategory(TaxCategory taxCategory);

        /// <summary>
        /// Deletes a tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        void DeleteTaxCategory(TaxCategory taxCategory);

    }
}
