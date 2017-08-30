using Denmakers.DreamSale.Model.Catalog;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Common
{
    public partial class AddressAttributeModel
    {
        public AddressAttributeModel()
        {
            Values = new List<AddressAttributeValueModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value for textboxes
        /// </summary>
        public string DefaultValue { get; set; }

        public AttributeControlType AttributeControlType { get; set; }

        public IList<AddressAttributeValueModel> Values { get; set; }
    }

    public partial class AddressAttributeValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsPreSelected { get; set; }
    }
}
