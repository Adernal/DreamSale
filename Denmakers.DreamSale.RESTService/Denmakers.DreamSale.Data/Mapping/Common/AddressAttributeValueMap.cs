using Denmakers.DreamSale.Model.Common;

namespace Denmakers.DreamSale.Data.Mapping.Common
{
    public partial class AddressAttributeValueMap : DreamSaleEntityTypeConfiguration<AddressAttributeValue>
    {
        public AddressAttributeValueMap()
        {
            this.ToTable("AddressAttributeValue");
            this.HasKey(aav => aav.Id);
            this.Property(aav => aav.Name).IsRequired().HasMaxLength(400);

            this.HasRequired(aav => aav.AddressAttribute)
                .WithMany(aa => aa.AddressAttributeValues)
                .HasForeignKey(aav => aav.AddressAttributeId);
        }
    }
}