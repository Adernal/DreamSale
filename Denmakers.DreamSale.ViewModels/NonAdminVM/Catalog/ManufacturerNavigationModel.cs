using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class ManufacturerNavigationModel
    {
        public ManufacturerNavigationModel()
        {
            this.Manufacturers = new List<ManufacturerBriefInfoModel>();
        }

        public int Id { get; set; }

        public IList<ManufacturerBriefInfoModel> Manufacturers { get; set; }

        public int TotalManufacturers { get; set; }
    }

    public partial class ManufacturerBriefInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SeName { get; set; }

        public bool IsActive { get; set; }
    }
}
