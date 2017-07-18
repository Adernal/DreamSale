using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.Validators.Vendors
{
    public partial class VendorListVM
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        [AllowHtml]
        public string SearchName { get; set; }
    }
}
