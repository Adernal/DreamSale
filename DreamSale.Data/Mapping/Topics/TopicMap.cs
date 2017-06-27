using DreamSale.Model.Topics;

namespace DreamSale.Data.Mapping.Topics
{
    public class TopicMap : DreamSaleEntityTypeConfiguration<Topic>
    {
        public TopicMap()
        {
            this.ToTable("Topic");
            this.HasKey(t => t.Id);
        }
    }
}
