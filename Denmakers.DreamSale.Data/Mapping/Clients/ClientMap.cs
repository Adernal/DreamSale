using Denmakers.DreamSale.Model.Clients;

namespace Denmakers.DreamSale.Data.Mapping.Clients
{
    public partial class ClientMap : DreamSaleEntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            this.ToTable("Client");
            this.HasKey(c => c.Id);

            //this.HasRequired(a => a.Name).WithMany().HasForeignKey(x => x.AddressId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Name);
            Property(c => c.AllowedOrigin).HasMaxLength(100);

        }
    }
}
