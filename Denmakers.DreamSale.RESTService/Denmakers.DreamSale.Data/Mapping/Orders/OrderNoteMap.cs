using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Data.Mapping.Orders
{
    public partial class OrderNoteMap : DreamSaleEntityTypeConfiguration<OrderNote>
    {
        public OrderNoteMap()
        {
            this.ToTable("OrderNote");
            this.HasKey(on => on.Id);
            this.Property(on => on.Note).IsRequired();

            this.HasRequired(on => on.Order)
                .WithMany(o => o.OrderNotes)
                .HasForeignKey(on => on.OrderId);
        }
    }
}