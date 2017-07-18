using Denmakers.DreamSale.Model.Localization;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Localization
{
    /// <summary>
    /// Localization manager interface
    /// </summary>
    public partial interface ILocalizationService
    {
        LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId);

        LocaleStringResource GetLocaleStringResourceByName(string resourceName);

        LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId, bool logIfNotFound = true);

        IList<LocaleStringResource> GetAllResources(int languageId);

        Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId);

        string GetResource(string resourceKey);

        string GetResource(string resourceKey, int languageId, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);

        string ExportResourcesToXml(Language language);

        void ImportResourcesFromXml(Language language, string xml, bool updateExistingResources = true);

        void InsertLocaleStringResource(LocaleStringResource localeStringResource);

        void UpdateLocaleStringResource(LocaleStringResource localeStringResource);

        void DeleteLocaleStringResource(LocaleStringResource localeStringResource);
    }
}
