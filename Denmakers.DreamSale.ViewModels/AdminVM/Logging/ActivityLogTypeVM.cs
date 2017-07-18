using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Logging
{
    public partial class ActivityLogTypeVM
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Enabled")]
        public bool Enabled { get; set; }
    }
}
