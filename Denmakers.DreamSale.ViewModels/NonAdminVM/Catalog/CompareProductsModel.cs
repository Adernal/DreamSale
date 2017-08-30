using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class CompareProductsModel
    {
        public CompareProductsModel()
        {
            Products = new List<ProductOverviewModel>();
        }
        public int Id { get; set; }
        public IList<ProductOverviewModel> Products { get; set; }

        public bool IncludeShortDescriptionInCompareProducts { get; set; }
        public bool IncludeFullDescriptionInCompareProducts { get; set; }
    }
}
