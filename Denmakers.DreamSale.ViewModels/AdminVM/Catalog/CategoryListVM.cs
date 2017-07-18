using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class CategoryListVM
    {
        #region Properties
        public Dictionary<string, object> CustomProperties { get; set; }
        [DisplayName("Category name")]
        [AllowHtml]
        public string SearchCategoryName { get; set; }

        [DisplayName("Store")]
        public int SearchStoreId { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        #endregion

        public CategoryListVM()
        {
            AvailableStores = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }


    }
}
