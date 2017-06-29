using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.Data.Mapping.Catalog
{
    public partial class TierPriceMap : DreamSaleEntityTypeConfiguration<TierPrice>
    {
        public TierPriceMap()
        {
            this.ToTable("TierPrice");
            this.HasKey(tp => tp.Id);
            this.Property(tp => tp.Price).HasPrecision(18, 4);

            this.HasRequired(tp => tp.Product)
                .WithMany(p => p.TierPrices)
                .HasForeignKey(tp => tp.ProductId);

            this.HasOptional(tp => tp.CustomerRole)
                .WithMany()
                .HasForeignKey(tp => tp.CustomerRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}