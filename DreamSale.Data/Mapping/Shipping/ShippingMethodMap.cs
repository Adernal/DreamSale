using DreamSale.Model.Shipping;

namespace DreamSale.Data.Mapping.Shipping
{
    public class ShippingMethodMap : DreamSaleEntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodMap()
        {
            this.ToTable("ShippingMethod");
            this.HasKey(sm => sm.Id);
            this.Property(sm => sm.Name).IsRequired().HasMaxLength(400);

            this.HasMany(sm => sm.RestrictedCountries)
                .WithMany(c => c.RestrictedShippingMethods)
                .Map(m => m.ToTable("ShippingMethodRestrictions"));
        }
    }
}
