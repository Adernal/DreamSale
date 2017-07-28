using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Tax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Tax
{
    public partial class TaxCategoryService : ITaxCategoryService
    {
        #region Fields

        private readonly IRepository<TaxCategory> _taxCategoryRepository;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public TaxCategoryService(IRepository<TaxCategory> taxCategoryRepository, IUnitOfWork unitOfWork)
        {
            //_cacheManager = cacheManager;
            this._taxCategoryRepository = taxCategoryRepository;
            //this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all tax categories
        /// </summary>
        /// <returns>Tax categories</returns>
        public virtual IList<TaxCategory> GetAllTaxCategories()
        {
            var query = from tc in _taxCategoryRepository.Table
                        orderby tc.DisplayOrder, tc.Id
                        select tc;
            var taxCategories = query.ToList();
            return taxCategories;
        }

        /// <summary>
        /// Gets a tax category
        /// </summary>
        /// <param name="taxCategoryId">Tax category identifier</param>
        /// <returns>Tax category</returns>
        public virtual TaxCategory GetTaxCategoryById(int taxCategoryId)
        {
            if (taxCategoryId == 0)
                return null;

            return _taxCategoryRepository.GetById(taxCategoryId);
        }

        /// <summary>
        /// Inserts a tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        public virtual void InsertTaxCategory(TaxCategory taxCategory)
        {
            if (taxCategory == null)
                throw new ArgumentNullException("taxCategory");

            _taxCategoryRepository.Insert(taxCategory);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        public virtual void UpdateTaxCategory(TaxCategory taxCategory)
        {
            if (taxCategory == null)
                throw new ArgumentNullException("taxCategory");

            _taxCategoryRepository.Update(taxCategory);
        }

        /// <summary>
        /// Deletes a tax category
        /// </summary>
        /// <param name="taxCategory">Tax category</param>
        public virtual void DeleteTaxCategory(TaxCategory taxCategory)
        {
            if (taxCategory == null)
                throw new ArgumentNullException("taxCategory");

            _taxCategoryRepository.Delete(taxCategory);
            //_unitOfWork.Commit();
        }
        #endregion
    }
}
