using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class ManufacturerListVM
    {
        #region Properties
        public Dictionary<string, object> CustomProperties { get; set; }
        [DisplayName("Manufacturer name")]
        [AllowHtml]
        public string SearchManufacturerName { get; set; }

        [DisplayName("Store")]
        public int SearchStoreId { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        #endregion

        public ManufacturerListVM()
        {
            AvailableStores = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }
    }
}
