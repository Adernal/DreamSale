using Denmakers.DreamSale.Model.Customers;

namespace Denmakers.DreamSale.Data.Mapping.Customers
{
    public partial class CustomerAttributeMap : DreamSaleEntityTypeConfiguration<CustomerAttribute>
    {
        public CustomerAttributeMap()
        {
            this.ToTable("CustomerAttribute");
            this.HasKey(ca => ca.Id);
            this.Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            this.Ignore(ca => ca.AttributeControlType);
        }
    }
}