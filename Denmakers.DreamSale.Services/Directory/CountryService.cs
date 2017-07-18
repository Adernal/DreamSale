using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Directory
{
    public partial class CountryService : ICountryService
    {

        #region Fields

        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public CountryService(/*ICacheManager cacheManager,*/
            IRepository<Country> countryRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IStoreContext storeContext,
            ISettingService settingService,
            IUnitOfWork unitOfWork,
            ILanguageService languageService, 
            ILocalizedEntityService localizedEntityService
            )
        {
            //this._cacheManager = cacheManager;
            this._countryRepository = countryRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._storeContext = storeContext;
            this._settingService = settingService;
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
            this._unitOfWork = unitOfWork;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a country
        /// </summary>
        /// <param name="country">Country</param>
        public virtual void DeleteCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            _countryRepository.Delete(country);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all countries
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Countries</returns>
        public virtual IList<Country> GetAllCountries(int languageId = 0, bool showHidden = false)
        {
            var query = _countryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name);

            if (!showHidden && !_catalogSettings.IgnoreStoreLimitations)
            {
                //Store mapping
                var currentStoreId = _storeContext.CurrentStore.Id;
                query = from c in query
                        join sc in _storeMappingRepository.Table
                        on new { c1 = c.Id, c2 = "Country" } equals new { c1 = sc.EntityId, c2 = sc.EntityName } into c_sc
                        from sc in c_sc.DefaultIfEmpty()
                        where !c.LimitedToStores || currentStoreId == sc.StoreId
                        select c;

                //only distinct entities (group by ID)
                query = from c in query
                        group c by c.Id
                            into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name);
            }

            var countries = query.ToList();

            if (languageId > 0)
            {
                //we should sort countries by localized names when they have the same display order
                countries = countries
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.GetLocalized(x => x.Name, languageId, _languageService, _localizedEntityService))
                    .ToList();
            }
            return countries;
        }

        /// <summary>
        /// Gets all countries that allow billing
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Countries</returns>
        public virtual IList<Country> GetAllCountriesForBilling(int languageId = 0, bool showHidden = false)
        {
            return GetAllCountries(languageId, showHidden).Where(c => c.AllowsBilling).ToList();
        }

        /// <summary>
        /// Gets all countries that allow shipping
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Countries</returns>
        public virtual IList<Country> GetAllCountriesForShipping(int languageId = 0, bool showHidden = false)
        {
            return GetAllCountries(languageId, showHidden).Where(c => c.AllowsShipping).ToList();
        }

        /// <summary>
        /// Gets a country 
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Country</returns>
        public virtual Country GetCountryById(int countryId)
        {
            if (countryId == 0)
                return null;

            return _countryRepository.GetById(countryId);
        }

        /// <summary>
        /// Get countries by identifiers
        /// </summary>
        /// <param name="countryIds">Country identifiers</param>
        /// <returns>Countries</returns>
        public virtual IList<Country> GetCountriesByIds(int[] countryIds)
        {
            if (countryIds == null || countryIds.Length == 0)
                return new List<Country>();

            var query = from c in _countryRepository.Table
                        where countryIds.Contains(c.Id)
                        select c;
            var countries = query.ToList();
            //sort by passed identifiers
            var sortedCountries = new List<Country>();
            foreach (int id in countryIds)
            {
                var country = countries.Find(x => x.Id == id);
                if (country != null)
                    sortedCountries.Add(country);
            }
            return sortedCountries;
        }

        /// <summary>
        /// Gets a country by two letter ISO code
        /// </summary>
        /// <param name="twoLetterIsoCode">Country two letter ISO code</param>
        /// <returns>Country</returns>
        public virtual Country GetCountryByTwoLetterIsoCode(string twoLetterIsoCode)
        {
            if (String.IsNullOrEmpty(twoLetterIsoCode))
                return null;

            var query = from c in _countryRepository.Table
                        where c.TwoLetterIsoCode == twoLetterIsoCode
                        select c;
            var country = query.FirstOrDefault();
            return country;
        }

        /// <summary>
        /// Gets a country by three letter ISO code
        /// </summary>
        /// <param name="threeLetterIsoCode">Country three letter ISO code</param>
        /// <returns>Country</returns>
        public virtual Country GetCountryByThreeLetterIsoCode(string threeLetterIsoCode)
        {
            if (String.IsNullOrEmpty(threeLetterIsoCode))
                return null;

            var query = from c in _countryRepository.Table
                        where c.ThreeLetterIsoCode == threeLetterIsoCode
                        select c;
            var country = query.FirstOrDefault();
            return country;
        }

        /// <summary>
        /// Inserts a country
        /// </summary>
        /// <param name="country">Country</param>
        public virtual void InsertCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            _countryRepository.Insert(country);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the country
        /// </summary>
        /// <param name="country">Country</param>
        public virtual void UpdateCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            _countryRepository.Update(country);
        }

        #endregion
    }
}
