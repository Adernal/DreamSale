using Denmakers.DreamSale.Model.Seo;

namespace Denmakers.DreamSale.Data.Mapping.Seo
{
    public partial class UrlRecordMap : DreamSaleEntityTypeConfiguration<UrlRecord>
    {
        public UrlRecordMap()
        {
            this.ToTable("UrlRecord");
            this.HasKey(lp => lp.Id);

            this.Property(lp => lp.EntityName).IsRequired().HasMaxLength(400);
            this.Property(lp => lp.Slug).IsRequired().HasMaxLength(400);
        }
    }
}