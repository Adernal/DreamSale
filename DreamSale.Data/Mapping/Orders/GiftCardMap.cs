using DreamSale.Model.Orders;

namespace DreamSale.Data.Mapping.Orders
{
    public partial class GiftCardMap : DreamSaleEntityTypeConfiguration<GiftCard>
    {
        public GiftCardMap()
        {
            this.ToTable("GiftCard");
            this.HasKey(gc => gc.Id);

            this.Property(gc => gc.Amount).HasPrecision(18, 4);

            this.Ignore(gc => gc.GiftCardType);

            this.HasOptional(gc => gc.PurchasedWithOrderItem)
                .WithMany(orderItem => orderItem.AssociatedGiftCards)
                .HasForeignKey(gc => gc.PurchasedWithOrderItemId);
        }
    }
}