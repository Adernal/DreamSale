using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Common;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Attributes
{
    public partial interface IGenericAttributeService
    {
        
        void DeleteAttribute(GenericAttribute attribute);

        
        void DeleteAttributes(IList<GenericAttribute> attributes);

        
        GenericAttribute GetAttributeById(int attributeId);

        
        void InsertAttribute(GenericAttribute attribute);

        
        void UpdateAttribute(GenericAttribute attribute);

        
        IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup);

        
        void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value, int storeId = 0);
    }
}
