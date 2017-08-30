using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public class CategorySimpleModel
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string SeName { get; set; }

        public int? NumberOfProducts { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public List<CategorySimpleModel> SubCategories { get; set; }
    }
}
