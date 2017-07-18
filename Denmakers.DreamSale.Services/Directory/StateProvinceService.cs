using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Directory
{
    public partial class StateProvinceService : IStateProvinceService
    {

        #region Fields

        private readonly IRepository<StateProvince> _stateProvinceRepository;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public StateProvinceService(/*ICacheManager cacheManager,*/
            IRepository<StateProvince> stateProvinceRepository,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IUnitOfWork unitOfWork)
        {
            //_cacheManager = cacheManager;
            this._stateProvinceRepository = stateProvinceRepository;
            this._unitOfWork = unitOfWork;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        public virtual StateProvince GetStateProvinceById(int stateProvinceId)
        {
            if (stateProvinceId == 0)
                return null;

            return _stateProvinceRepository.GetById(stateProvinceId);
        }

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        public virtual StateProvince GetStateProvinceByAbbreviation(string abbreviation)
        {
            var query = from sp in _stateProvinceRepository.Table
                        where sp.Abbreviation == abbreviation
                        select sp;
            var stateProvince = query.FirstOrDefault();
            return stateProvince;
        }

        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="languageId">Language identifier. It's used to sort states by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        public virtual IList<StateProvince> GetStateProvincesByCountryId(int countryId, int languageId = 0, bool showHidden = false)
        {
            var query = from sp in _stateProvinceRepository.Table
                        orderby sp.DisplayOrder, sp.Name
                        where sp.CountryId == countryId &&
                        (showHidden || sp.Published)
                        select sp;
            var stateProvinces = query.ToList();

            if (languageId > 0)
            {
                //we should sort states by localized names when they have the same display order
                stateProvinces = stateProvinces
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.GetLocalized(x => x.Name, languageId, _languageService, _localizedEntityService))
                    .ToList();
            }
            return stateProvinces;
        }

        /// <summary>
        /// Gets all states/provinces
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        public virtual IList<StateProvince> GetStateProvinces(bool showHidden = false)
        {
            var query = from sp in _stateProvinceRepository.Table
                        orderby sp.CountryId, sp.DisplayOrder, sp.Name
                        where showHidden || sp.Published
                        select sp;
            var stateProvinces = query.ToList();
            return stateProvinces;
        }

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        public virtual void InsertStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Insert(stateProvince);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        public virtual void UpdateStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Update(stateProvince);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="stateProvince">The state/province</param>
        public virtual void DeleteStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Delete(stateProvince);

            _unitOfWork.Commit();
        }
        #endregion
    }
}
