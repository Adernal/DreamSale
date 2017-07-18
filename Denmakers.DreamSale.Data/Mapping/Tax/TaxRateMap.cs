using Denmakers.DreamSale.Model.Tax;

namespace Denmakers.DreamSale.Data.Mapping.Tax
{
    public class TaxRateMap : DreamSaleEntityTypeConfiguration<TaxRate>
    {
        public TaxRateMap()
        {
            this.ToTable("TaxRate");
            this.HasKey(tr => tr.Id);
            this.Property(tr => tr.Percentage).HasPrecision(18, 4);
        }
    }
}
