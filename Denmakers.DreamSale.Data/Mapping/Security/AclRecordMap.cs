using Denmakers.DreamSale.Model.Security;

namespace Denmakers.DreamSale.Data.Mapping.Security
{
    public partial class AclRecordMap : DreamSaleEntityTypeConfiguration<AclRecord>
    {
        public AclRecordMap()
        {
            this.ToTable("AclRecord");
            this.HasKey(ar => ar.Id);

            this.Property(ar => ar.EntityName).IsRequired().HasMaxLength(400);

            this.HasRequired(ar => ar.CustomerRole)
                .WithMany()
                .HasForeignKey(ar => ar.CustomerRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}