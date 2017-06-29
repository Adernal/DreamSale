using Denmakers.DreamSale.Model.Shipping;

namespace Denmakers.DreamSale.Data.Mapping.Shipping
{
    public partial class ShipmentItemMap : DreamSaleEntityTypeConfiguration<ShipmentItem>
    {
        public ShipmentItemMap()
        {
            this.ToTable("ShipmentItem");
            this.HasKey(si => si.Id);

            this.HasRequired(si => si.Shipment)
                .WithMany(s => s.ShipmentItems)
                .HasForeignKey(si => si.ShipmentId);
        }
    }
}