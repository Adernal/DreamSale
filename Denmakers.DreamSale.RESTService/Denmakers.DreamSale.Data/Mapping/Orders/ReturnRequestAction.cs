using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Data.Mapping.Orders
{
    public partial class ReturnRequestActionMap : DreamSaleEntityTypeConfiguration<ReturnRequestAction>
    {
        public ReturnRequestActionMap()
        {
            this.ToTable("ReturnRequestAction");
            this.HasKey(rra => rra.Id);
            this.Property(rra => rra.Name).IsRequired().HasMaxLength(400);
        }
    }
}