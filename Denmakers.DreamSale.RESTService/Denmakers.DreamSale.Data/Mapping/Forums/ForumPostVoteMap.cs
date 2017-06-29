using Denmakers.DreamSale.Model.Forums;

namespace Denmakers.DreamSale.Data.Mapping.Forums
{
    public partial class ForumPostVoteMap : DreamSaleEntityTypeConfiguration<ForumPostVote>
    {
        public ForumPostVoteMap()
        {
            this.ToTable("Forums_PostVote");
            this.HasKey(fpv => fpv.Id);

            this.HasRequired(fpv => fpv.ForumPost)
                .WithMany()
                .HasForeignKey(fpv => fpv.ForumPostId)
                .WillCascadeOnDelete(true);
        }
    }
}
