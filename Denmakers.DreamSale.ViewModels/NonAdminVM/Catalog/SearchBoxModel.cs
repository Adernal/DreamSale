namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class SearchBoxModel
    {
        public int Id { get; set; }
        public bool AutoCompleteEnabled { get; set; }
        public bool ShowProductImagesInSearchAutoComplete { get; set; }
        public int SearchTermMinimumLength { get; set; }
    }
}
