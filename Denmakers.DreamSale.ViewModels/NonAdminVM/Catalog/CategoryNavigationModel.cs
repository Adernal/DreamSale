using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class CategoryNavigationModel
    {
        public CategoryNavigationModel()
        {
            Categories = new List<CategorySimpleModel>();
        }
        public int Id { get; set; }
        public int CurrentCategoryId { get; set; }
        public List<CategorySimpleModel> Categories { get; set; }
    }
}
