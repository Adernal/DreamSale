using DreamSale.Model.Catalog;

namespace DreamSale.Data.Mapping.Catalog
{
    public partial class StockQuantityHistoryMap : DreamSaleEntityTypeConfiguration<StockQuantityHistory>
    {
        public StockQuantityHistoryMap()
        {
            this.ToTable("StockQuantityHistory");
            this.HasKey(historyEntry => historyEntry.Id);

            this.HasRequired(historyEntry => historyEntry.Product)
                .WithMany()
                .HasForeignKey(historyEntry => historyEntry.ProductId)
                .WillCascadeOnDelete(true);
        }
    }
}