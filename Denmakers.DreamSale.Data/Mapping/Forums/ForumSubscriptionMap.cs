using Denmakers.DreamSale.Model.Forums;

namespace Denmakers.DreamSale.Data.Mapping.Forums
{
    public partial class ForumSubscriptionMap : DreamSaleEntityTypeConfiguration<ForumSubscription>
    {
        public ForumSubscriptionMap()
        {
            this.ToTable("Forums_Subscription");
            this.HasKey(fs => fs.Id);

            this.HasRequired(fs => fs.Customer)
                .WithMany()
                .HasForeignKey(fs => fs.CustomerId)
                .WillCascadeOnDelete(false);
        }
    }
}
