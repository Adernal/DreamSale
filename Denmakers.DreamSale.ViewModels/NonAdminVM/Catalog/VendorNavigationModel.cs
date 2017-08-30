using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class VendorNavigationModel
    {
        public VendorNavigationModel()
        {
            this.Vendors = new List<VendorBriefInfoModel>();
        }
        public int Id { get; set; }
        public IList<VendorBriefInfoModel> Vendors { get; set; }

        public int TotalVendors { get; set; }
    }

    public partial class VendorBriefInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SeName { get; set; }
    }
}
