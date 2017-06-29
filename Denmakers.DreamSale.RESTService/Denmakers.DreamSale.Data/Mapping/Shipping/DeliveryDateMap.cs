using Denmakers.DreamSale.Model.Shipping;

namespace Denmakers.DreamSale.Data.Mapping.Shipping
{
    public class DeliveryDateMap : DreamSaleEntityTypeConfiguration<DeliveryDate>
    {
        public DeliveryDateMap()
        {
            this.ToTable("DeliveryDate");
            this.HasKey(dd => dd.Id);
            this.Property(dd => dd.Name).IsRequired().HasMaxLength(400);
        }
    }
}
