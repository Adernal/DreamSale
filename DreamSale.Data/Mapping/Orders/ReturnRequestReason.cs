using DreamSale.Model.Orders;

namespace DreamSale.Data.Mapping.Orders
{
    public partial class ReturnRequestReasonMap : DreamSaleEntityTypeConfiguration<ReturnRequestReason>
    {
        public ReturnRequestReasonMap()
        {
            this.ToTable("ReturnRequestReason");
            this.HasKey(rrr => rrr.Id);
            this.Property(rrr => rrr.Name).IsRequired().HasMaxLength(400);
        }
    }
}