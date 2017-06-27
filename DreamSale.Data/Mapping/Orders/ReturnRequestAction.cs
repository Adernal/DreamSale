using DreamSale.Model.Orders;

namespace DreamSale.Data.Mapping.Orders
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