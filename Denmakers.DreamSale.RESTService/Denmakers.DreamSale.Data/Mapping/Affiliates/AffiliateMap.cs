using Denmakers.DreamSale.Model.Affiliates;

namespace Denmakers.DreamSale.Data.Mapping.Affiliates
{
    public partial class AffiliateMap : DreamSaleEntityTypeConfiguration<Affiliate>
    {
        public AffiliateMap()
        {
            this.ToTable("Affiliate");
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Address).WithMany().HasForeignKey(x => x.AddressId).WillCascadeOnDelete(false);
        }
    }
}