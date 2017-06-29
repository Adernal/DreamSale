using Denmakers.DreamSale.Model.Shipping;

namespace Denmakers.DreamSale.Data.Mapping.Shipping
{
    public class WarehouseMap : DreamSaleEntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            this.ToTable("Warehouse");
            this.HasKey(wh => wh.Id);
            this.Property(wh => wh.Name).IsRequired().HasMaxLength(400);
        }
    }
}
