using Denmakers.DreamSale.Model.Polls;

namespace Denmakers.DreamSale.Data.Mapping.Polls
{
    public partial class PollVotingRecordMap : DreamSaleEntityTypeConfiguration<PollVotingRecord>
    {
        public PollVotingRecordMap()
        {
            this.ToTable("PollVotingRecord");
            this.HasKey(pr => pr.Id);

            this.HasRequired(pvr => pvr.PollAnswer)
                .WithMany(pa => pa.PollVotingRecords)
                .HasForeignKey(pvr => pvr.PollAnswerId);

            this.HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);
        }
    }
}