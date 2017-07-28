using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Localization;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Localization
{
    /// <summary>
    /// Language service
    /// </summary>
    public partial class LanguageService : ILanguageService
    {
       
        #region Fields

        private readonly IRepository<Language> _languageRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ISettingService _settingService;
        private readonly LocalizationSettings _localizationSettings;
        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public LanguageService(IRepository<Language> languageRepository,
            IStoreMappingService storeMappingService,
            ISettingService settingService,
            LocalizationSettings localizationSettings,
            IUnitOfWork unitOfWork)
        {
            this._languageRepository = languageRepository;
            this._storeMappingService = storeMappingService;
            this._settingService = settingService;
            this._localizationSettings = localizationSettings;
            //this._unitOfWork = unitOfWork;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Deletes a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void DeleteLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            
            //update default admin area language (if required)
            if (_localizationSettings.DefaultAdminLanguageId == language.Id)
            {
                foreach (var activeLanguage in GetAllLanguages())
                {
                    if (activeLanguage.Id != language.Id)
                    {
                        _localizationSettings.DefaultAdminLanguageId = activeLanguage.Id;
                        _settingService.SaveSetting(_localizationSettings);
                        break;
                    }
                }
            }
            
            _languageRepository.Delete(language);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Languages</returns>
        public virtual IList<Language> GetAllLanguages(bool showHidden = false, int storeId = 0)
        {
            var query = _languageRepository.Table;
            if (!showHidden)
                query = query.Where(l => l.Published);
            query = query.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
            var languages = query.ToList();

            //store mapping
            if (storeId > 0)
            {
                languages = languages
                    .Where(l => _storeMappingService.Authorize(l, storeId))
                    .ToList();
            }
            return languages;
        }

        /// <summary>
        /// Gets a language
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Language</returns>
        public virtual Language GetLanguageById(int languageId)
        {
            if (languageId == 0)
                return null;
            
            return _languageRepository.GetById(languageId);
        }

        /// <summary>
        /// Inserts a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void InsertLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");

            _languageRepository.Insert(language);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void UpdateLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            
            //update language
            _languageRepository.Update(language);

            //_unitOfWork.Commit();
        }

        #endregion
    }
}
