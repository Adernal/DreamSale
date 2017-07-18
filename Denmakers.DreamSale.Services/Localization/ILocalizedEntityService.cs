using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Localization;
using System;
using System.Linq.Expressions;

namespace Denmakers.DreamSale.Services.Localization
{
    public partial interface ILocalizedEntityService
    {
        
        LocalizedProperty GetLocalizedPropertyById(int localizedPropertyId);

        string GetLocalizedValue(int languageId, int entityId, string localeKeyGroup, string localeKey);

        void InsertLocalizedProperty(LocalizedProperty localizedProperty);

        void UpdateLocalizedProperty(LocalizedProperty localizedProperty);

        void DeleteLocalizedProperty(LocalizedProperty localizedProperty);

        void SaveLocalizedValue<T>(T entity,Expression<Func<T, string>> keySelector, string localeValue, int languageId) 
            where T : BaseEntity, ILocalizedEntity;

        void SaveLocalizedValue<T, TPropType>(T entity, Expression<Func<T, TPropType>> keySelector, TPropType localeValue, int languageId)
            where T : BaseEntity, ILocalizedEntity;
    }
}
