using Denmakers.DreamSale.Model.Tax;

namespace Denmakers.DreamSale.Data.Mapping.Tax
{
    public class TaxCategoryMap : DreamSaleEntityTypeConfiguration<TaxCategory>
    {
        public TaxCategoryMap()
        {
            this.ToTable("TaxCategory");
            this.HasKey(tc => tc.Id);
            this.Property(tc => tc.Name).IsRequired().HasMaxLength(400);
        }
    }
}
