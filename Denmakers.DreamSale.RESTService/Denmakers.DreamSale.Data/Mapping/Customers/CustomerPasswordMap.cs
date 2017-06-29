using Denmakers.DreamSale.Model.Customers;

namespace Denmakers.DreamSale.Data.Mapping.Customers
{
    public partial class CustomerPasswordMap : DreamSaleEntityTypeConfiguration<CustomerPassword>
    {
        public CustomerPasswordMap()
        {
            this.ToTable("CustomerPassword");
            this.HasKey(password => password.Id);

            this.HasRequired(password => password.Customer)
                .WithMany()
                .HasForeignKey(password => password.CustomerId);

            this.Ignore(password => password.PasswordFormat);
        }
    }
}