using Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Media;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Vendors
{
    public partial class VendorModel
    {
        public VendorModel()
        {
            PictureModel = new PictureModel();
            Products = new List<ProductOverviewModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        public bool AllowCustomersToContactVendors { get; set; }

        public PictureModel PictureModel { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public IList<ProductOverviewModel> Products { get; set; }
    }
}
