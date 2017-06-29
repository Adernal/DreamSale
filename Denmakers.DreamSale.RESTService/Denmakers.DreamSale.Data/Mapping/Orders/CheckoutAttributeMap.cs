using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Data.Mapping.Orders
{
    public partial class CheckoutAttributeMap : DreamSaleEntityTypeConfiguration<CheckoutAttribute>
    {
        public CheckoutAttributeMap()
        {
            this.ToTable("CheckoutAttribute");
            this.HasKey(ca => ca.Id);
            this.Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            this.Ignore(ca => ca.AttributeControlType);
        }
    }
}