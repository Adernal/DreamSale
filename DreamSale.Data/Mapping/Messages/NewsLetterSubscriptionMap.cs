using DreamSale.Model.Messages;

namespace DreamSale.Data.Mapping.Messages
{
    public partial class NewsLetterSubscriptionMap : DreamSaleEntityTypeConfiguration<NewsLetterSubscription>
    {
        public NewsLetterSubscriptionMap()
        {
            this.ToTable("NewsLetterSubscription");
            this.HasKey(nls => nls.Id);

            this.Property(nls => nls.Email).IsRequired().HasMaxLength(255);
        }
    }
}