using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.Data.Mapping.Catalog
{
    public partial class ProductAttributeMap : DreamSaleEntityTypeConfiguration<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            this.ToTable("ProductAttribute");
            this.HasKey(pa => pa.Id);
            this.Property(pa => pa.Name).IsRequired();
        }
    }
}