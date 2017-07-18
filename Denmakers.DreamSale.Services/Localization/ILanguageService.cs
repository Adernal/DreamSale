using Denmakers.DreamSale.Model.Localization;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Localization
{
    public partial interface ILanguageService
    {
        IList<Language> GetAllLanguages(bool showHidden = false, int storeId = 0);
        
        Language GetLanguageById(int languageId);
        
        void InsertLanguage(Language language);

        void UpdateLanguage(Language language);

        void DeleteLanguage(Language language);
    }
}
