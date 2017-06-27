using DreamSale.Model.Media;

namespace DreamSale.Data.Mapping.Media
{
    public partial class DownloadMap : DreamSaleEntityTypeConfiguration<Download>
    {
        public DownloadMap()
        {
            this.ToTable("Download");
            this.HasKey(p => p.Id);
            this.Property(p => p.DownloadBinary).IsMaxLength();
        }
    }
}