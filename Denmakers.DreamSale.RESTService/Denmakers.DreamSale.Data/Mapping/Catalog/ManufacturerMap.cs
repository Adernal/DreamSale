using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.Data.Mapping.Catalog
{
    public partial class ManufacturerMap : DreamSaleEntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerMap()
        {
            this.ToTable("Manufacturer");
            this.HasKey(m => m.Id);
            this.Property(m => m.Name).IsRequired().HasMaxLength(400);
            this.Property(m => m.MetaKeywords).HasMaxLength(400);
            this.Property(m => m.MetaTitle).HasMaxLength(400);
            this.Property(m => m.PriceRanges).HasMaxLength(400);
            this.Property(m => m.PageSizeOptions).HasMaxLength(200);
        }
    }
}