using Denmakers.DreamSale.Model.Shipping;

namespace Denmakers.DreamSale.Data.Mapping.Shipping
{
    public class ProductAvailabilityRangeMap : DreamSaleEntityTypeConfiguration<ProductAvailabilityRange>
    {
        public ProductAvailabilityRangeMap()
        {
            this.ToTable("ProductAvailabilityRange");
            this.HasKey(range => range.Id);
            this.Property(range => range.Name).IsRequired().HasMaxLength(400);
        }
    }
}
