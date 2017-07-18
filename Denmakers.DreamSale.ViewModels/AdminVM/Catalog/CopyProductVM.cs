using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class CopyProductVM
    {
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Catalog.Products.Copy.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Catalog.Products.Copy.CopyImages")]
        public bool CopyImages { get; set; }

        [DisplayName("Admin.Catalog.Products.Copy.Published")]
        public bool Published { get; set; }

    }
}
