using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class ProductsByTagModel
    {
        public ProductsByTagModel()
        {
            Products = new List<ProductOverviewModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
        }
        public int Id { get; set; }
        public string TagName { get; set; }
        public string TagSeName { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public IList<ProductOverviewModel> Products { get; set; }
    }
}
