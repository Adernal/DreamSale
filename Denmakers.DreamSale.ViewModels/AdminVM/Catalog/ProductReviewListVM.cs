using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class ProductReviewListVM
    {
        #region Properties
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Created from")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("Created to")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        [DisplayName("Message")]
        [AllowHtml]
        public string SearchText { get; set; }

        [DisplayName("Store")]
        public int SearchStoreId { get; set; }

        [DisplayName("Product")]
        public int SearchProductId { get; set; }

        [DisplayName("Approved")]
        public int SearchApprovedId { get; set; }

        //vendor
        public bool IsLoggedInAsVendor { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableApprovedOptions { get; set; }
        #endregion

        public ProductReviewListVM()
        {
            AvailableStores = new List<SelectListItem>();
            AvailableApprovedOptions = new List<SelectListItem>();
            this.CustomProperties = new Dictionary<string, object>();
        }
    }
}
