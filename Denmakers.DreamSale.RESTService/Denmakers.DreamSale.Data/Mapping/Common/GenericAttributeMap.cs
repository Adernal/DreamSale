using Denmakers.DreamSale.Model.Common;

namespace Denmakers.DreamSale.Data.Mapping.Common
{
    public partial class GenericAttributeMap : DreamSaleEntityTypeConfiguration<GenericAttribute>
    {
        public GenericAttributeMap()
        {
            this.ToTable("GenericAttribute");
            this.HasKey(ga => ga.Id);

            this.Property(ga => ga.KeyGroup).IsRequired().HasMaxLength(400);
            this.Property(ga => ga.Key).IsRequired().HasMaxLength(400);
            this.Property(ga => ga.Value).IsRequired();
        }
    }
}