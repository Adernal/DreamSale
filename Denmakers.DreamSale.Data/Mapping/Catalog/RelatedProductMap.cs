using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.Data.Mapping.Catalog
{
    public partial class RelatedProductMap : DreamSaleEntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductMap()
        {
            this.ToTable("RelatedProduct");
            this.HasKey(c => c.Id);
        }
    }
}