using Denmakers.DreamSale.Model.Directory;

namespace Denmakers.DreamSale.Data.Mapping.Directory
{
    public partial class MeasureWeightMap : DreamSaleEntityTypeConfiguration<MeasureWeight>
    {
        public MeasureWeightMap()
        {
            this.ToTable("MeasureWeight");
            this.HasKey(m => m.Id);
            this.Property(m => m.Name).IsRequired().HasMaxLength(100);
            this.Property(m => m.SystemKeyword).IsRequired().HasMaxLength(100);
            this.Property(m => m.Ratio).HasPrecision(18, 8);
        }
    }
}