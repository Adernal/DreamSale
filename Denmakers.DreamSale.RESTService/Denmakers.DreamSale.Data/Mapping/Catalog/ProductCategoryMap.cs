using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.Data.Mapping.Catalog
{
    public partial class ProductCategoryMap : DreamSaleEntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            this.ToTable("Product_Category_Mapping");
            this.HasKey(pc => pc.Id);
            
            this.HasRequired(pc => pc.Category)
                .WithMany()
                .HasForeignKey(pc => pc.CategoryId);


            this.HasRequired(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
        }
    }
}