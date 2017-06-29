using Denmakers.DreamSale.Model.Polls;

namespace Denmakers.DreamSale.Data.Mapping.Polls
{
    public partial class PollMap : DreamSaleEntityTypeConfiguration<Poll>
    {
        public PollMap()
        {
            this.ToTable("Poll");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired();
            
            this.HasRequired(p => p.Language)
                .WithMany()
                .HasForeignKey(p => p.LanguageId).WillCascadeOnDelete(true);
        }
    }
}