using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Tax;
using Denmakers.DreamSale.Services.Configuration;
using System;
using System.Linq;

namespace Denmakers.DreamSale.Services.Tax
{
    public partial class CountryStateZipTaxRateService : ITaxRateService
    {
        #region Fields
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TaxRate> _taxRateRepository;
        private readonly ISettingService _settingService;
        private readonly FixedOrByCountryStateZipTaxSettings _countryStateZipSettings;
        private readonly IStoreContext _storeContext;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public CountryStateZipTaxRateService(IRepository<TaxRate> taxRateRepository, /*IUnitOfWork unitOfWork,*/ IStoreContext storeContext, ISettingService settingService)
        {
            //this._unitOfWork = unitOfWork;
            this._storeContext = storeContext;
            //this._cacheManager = cacheManager;
            this._taxRateRepository = taxRateRepository;
            this._settingService = settingService;
            this._countryStateZipSettings = _settingService.LoadSetting<FixedOrByCountryStateZipTaxSettings>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets all tax rates
        /// </summary>
        /// <returns>Tax rates</returns>
        public virtual IPagedList<TaxRate> GetAllTaxRates(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from tr in _taxRateRepository.Table
                        orderby tr.StoreId, tr.CountryId, tr.StateProvinceId, tr.Zip, tr.TaxCategoryId
                        select tr;
            var records = new PagedList<TaxRate>(query, pageIndex, pageSize);
            return records;
        }

        /// <summary>
        /// Gets a tax rate
        /// </summary>
        /// <param name="taxRateId">Tax rate identifier</param>
        /// <returns>Tax rate</returns>
        public virtual TaxRate GetTaxRateById(int taxRateId)
        {
            if (taxRateId == 0)
                return null;

            return _taxRateRepository.GetById(taxRateId);
        }

        /// <summary>
        /// Gets tax rate
        /// </summary>
        /// <param name="calculateTaxRequest">Tax calculation request</param>
        /// <returns>Tax</returns>
        public CalculateTaxResult GetTaxRate(CalculateTaxRequest calculateTaxRequest)
        {
            var result = new CalculateTaxResult();

            //choose the tax rate calculation method
            if (!_countryStateZipSettings.CountryStateZipEnabled)
            {
                //the tax rate calculation by fixed rate
                result = new CalculateTaxResult
                {
                    TaxRate = _settingService.GetSettingByKey<decimal>(string.Format("Tax.TaxProvider.FixedOrByCountryStateZip.TaxCategoryId{0}", calculateTaxRequest.TaxCategoryId))
                };
            }
            else
            {
                //the tax rate calculation by country & state & zip 

                if (calculateTaxRequest.Address == null)
                {
                    result.Errors.Add("Address is not set");
                    return result;
                }

                //first, load all tax rate records (cached) - loaded only once
                var allTaxRates = GetAllTaxRates()
                        .Select(x => new TaxRateForCaching
                        {
                            Id = x.Id,
                            StoreId = x.StoreId,
                            TaxCategoryId = x.TaxCategoryId,
                            CountryId = x.CountryId,
                            StateProvinceId = x.StateProvinceId,
                            Zip = x.Zip,
                            Percentage = x.Percentage
                        }
                        )
                        .ToList();

                var storeId = _storeContext.CurrentStore.Id;
                var taxCategoryId = calculateTaxRequest.TaxCategoryId;
                var countryId = calculateTaxRequest.Address.Country != null ? calculateTaxRequest.Address.Country.Id : 0;
                var stateProvinceId = calculateTaxRequest.Address.StateProvince != null
                    ? calculateTaxRequest.Address.StateProvince.Id
                    : 0;
                var zip = calculateTaxRequest.Address.ZipPostalCode;

                if (zip == null)
                    zip = string.Empty;
                zip = zip.Trim();

                var existingRates = allTaxRates.Where(taxRate => taxRate.CountryId == countryId && taxRate.TaxCategoryId == taxCategoryId).ToList();

                //filter by store
                //first, find by a store ID
                var matchedByStore = existingRates.Where(taxRate => storeId == taxRate.StoreId).ToList();

                //not found? use the default ones (ID == 0)
                if (!matchedByStore.Any())
                    matchedByStore.AddRange(existingRates.Where(taxRate => taxRate.StoreId == 0));

                //filter by state/province
                //first, find by a state ID
                var matchedByStateProvince = matchedByStore.Where(taxRate => stateProvinceId == taxRate.StateProvinceId).ToList();

                //not found? use the default ones (ID == 0)
                if (!matchedByStateProvince.Any())
                    matchedByStateProvince.AddRange(matchedByStore.Where(taxRate => taxRate.StateProvinceId == 0));

                //filter by zip
                var matchedByZip = matchedByStateProvince.Where(taxRate => (string.IsNullOrEmpty(zip) && string.IsNullOrEmpty(taxRate.Zip)) || zip.Equals(taxRate.Zip, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!matchedByZip.Any())
                    matchedByZip.AddRange(matchedByStateProvince.Where(taxRate => string.IsNullOrWhiteSpace(taxRate.Zip)));

                if (matchedByZip.Any())
                    result.TaxRate = matchedByZip[0].Percentage;
            }
            return result;
        }



        /// <summary>
        /// Inserts a tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        public virtual void InsertTaxRate(TaxRate taxRate)
        {
            if (taxRate == null)
                throw new ArgumentNullException("taxRate");

            _taxRateRepository.Insert(taxRate);

            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        public virtual void UpdateTaxRate(TaxRate taxRate)
        {
            if (taxRate == null)
                throw new ArgumentNullException("taxRate");

            _taxRateRepository.Update(taxRate);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate</param>
        public virtual void DeleteTaxRate(TaxRate taxRate)
        {
            if (taxRate == null)
                throw new ArgumentNullException("taxRate");

            _taxRateRepository.Delete(taxRate);

            //_unitOfWork.Commit();
        }
        #endregion
    }
}
