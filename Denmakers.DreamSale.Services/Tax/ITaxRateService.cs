using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Model.Tax;

namespace Denmakers.DreamSale.Services.Tax
{
    public partial interface ITaxRateService
    {
        /// <summary>
        /// Gets all tax rates
        /// </summary>
        /// <returns>Tax rates</returns>
        IPagedList<TaxRate> GetAllTaxRates(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a tax rate
        /// </summary>
        /// <param name="taxRateId">Tax rate identifier</param>
        /// <returns>Tax rate</returns>
        TaxRate GetTaxRateById(int taxRateId);

        /// <summary>
        /// Gets tax rate
        /// </summary>
        /// <param name="calculateTaxRequest">Tax calculation request</param>
        /// <returns>Tax</returns>
        CalculateTaxResult GetTaxRate(CalculateTaxRequest calculateTaxRequest);

        /// <summary>
        /// Inserts a tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        void InsertTaxRate(TaxRate taxRate);

        /// <summary>
        /// Updates the tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        void UpdateTaxRate(TaxRate taxRate);

        /// <summary>
        /// Deletes a tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        void DeleteTaxRate(TaxRate taxRate);
    }
}
