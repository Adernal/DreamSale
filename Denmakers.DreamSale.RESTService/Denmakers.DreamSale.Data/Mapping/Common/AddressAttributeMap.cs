using Denmakers.DreamSale.Model.Common;

namespace Denmakers.DreamSale.Data.Mapping.Common
{
    public partial class AddressAttributeMap : DreamSaleEntityTypeConfiguration<AddressAttribute>
    {
        public AddressAttributeMap()
        {
            this.ToTable("AddressAttribute");
            this.HasKey(aa => aa.Id);
            this.Property(aa => aa.Name).IsRequired().HasMaxLength(400);

            this.Ignore(aa => aa.AttributeControlType);
        }
    }
}