using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Data.Mapping.Orders
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