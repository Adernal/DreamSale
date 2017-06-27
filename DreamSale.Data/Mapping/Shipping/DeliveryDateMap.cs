using DreamSale.Model.Shipping;

namespace DreamSale.Data.Mapping.Shipping
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
