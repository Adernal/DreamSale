using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Localization;
using Denmakers.DreamSale.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Denmakers.DreamSale.Services.Localization
{
    /// <summary>
    /// Provides information about localization
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        #region Fields
        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly ILanguageService _languageService;
        private readonly ISettingService _settingService;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly CommonSettings _commonSettings;
        private readonly LocalizationSettings _localizationSettings;

        #endregion

        #region Ctor
        public LocalizationService(IRepository<LocaleStringResource> lsrRepository, ILanguageService languageService, IUnitOfWork unitOfWork, ISettingService settingService)
        {
            this._lsrRepository = lsrRepository;
            this._languageService = languageService;
            this._settingService = settingService;
            this._unitOfWork = unitOfWork;
            this._commonSettings = _settingService.LoadSetting<CommonSettings>();
            this._localizationSettings = _settingService.LoadSetting<LocalizationSettings>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException("localeStringResource");

            _lsrRepository.Delete(localeStringResource);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;

            return _lsrRepository.GetById(localeStringResourceId);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            return GetLocaleStringResourceByName(resourceName, 1);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId, bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId && lsr.ResourceName == resourceName
                        select lsr;
            var localeStringResource = query.FirstOrDefault();

            
            return localeStringResource;
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual IList<LocaleStringResource> GetAllResources(int languageId)
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;
            var locales = query.ToList();
            return locales;
        }

        /// <summary>
        /// Inserts a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException("localeStringResource");
            
            _lsrRepository.Insert(localeStringResource);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException("localeStringResource");

            _lsrRepository.Update(localeStringResource);

            
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual Dictionary<string, KeyValuePair<int,string>> GetAllResourceValues(int languageId)
        {
            //we use no tracking here for performance optimization
            //anyway records are loaded only for read-only operations
            var query = from l in _lsrRepository.TableNoTracking
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;
            var locales = query.ToList();
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }
            return dictionary;
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {
            return GetResource(resourceKey, 1);
        }
        
        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var resources = GetAllResourceValues(languageId);
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
            }
            else
            {
                var query = from l in _lsrRepository.Table
                            where l.ResourceName == resourceKey
                            && l.LanguageId == languageId
                            select l.ResourceValue;
                string lsr = query.FirstOrDefault();
                if (lsr != null)
                    result = lsr;
            }
            if (String.IsNullOrEmpty(result))
            {
                //if (logIfNotFound)
                //    _logger.Warning(string.Format("Resource string ({0}) is not found. Language ID = {1}", resourceKey, languageId));
                
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else
                {
                    if (!returnEmptyIfNotFound)
                        result = resourceKey;
                }
            }
            return result;
        }

        /// <summary>
        /// Export language resources to xml
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Result in XML format</returns>
        public virtual string ExportResourcesToXml(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Language");
            xmlWriter.WriteAttributeString("Name", language.Name);
            xmlWriter.WriteAttributeString("SupportedVersion", "1.0");


            var resources = GetAllResources(language.Id);
            foreach (var resource in resources)
            {
                xmlWriter.WriteStartElement("LocaleResource");
                xmlWriter.WriteAttributeString("Name", resource.ResourceName);
                xmlWriter.WriteElementString("Value", null, resource.ResourceValue);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        /// <summary>
        /// Import language resources from XML file
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="xml">XML</param>
        /// <param name="updateExistingResources">A value indicating whether to update existing resources</param>
        public virtual void ImportResourcesFromXml(Language language, string xml, bool updateExistingResources = true)
        {
            if (language == null)
                throw new ArgumentNullException("language");

            if (String.IsNullOrEmpty(xml))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var nodes = xmlDoc.SelectNodes(@"//Language/LocaleResource");
            foreach (XmlNode node in nodes)
            {
                string name = node.Attributes["Name"].InnerText.Trim();
                string value = "";
                var valueNode = node.SelectSingleNode("Value");
                if (valueNode != null)
                    value = valueNode.InnerText;

                if (String.IsNullOrEmpty(name))
                    continue;

                //do not use "Insert"/"Update" methods because they clear cache
                //let's bulk insert
                var resource = language.LocaleStringResources.FirstOrDefault(x => x.ResourceName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                if (resource != null)
                {
                    if (updateExistingResources)
                    {
                        resource.ResourceValue = value;
                    }
                }
                else
                {
                    language.LocaleStringResources.Add(
                        new LocaleStringResource
                        {
                            ResourceName = name,
                            ResourceValue = value
                        });
                }
            }
            _languageService.UpdateLanguage(language);
        }

        #endregion
    }
}
