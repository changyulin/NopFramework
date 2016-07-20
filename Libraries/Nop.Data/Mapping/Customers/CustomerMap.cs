using Nop.Core.Domain;
using Nop.Core.Domain.Customers;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            this.Ignore(c => c.PasswordFormat);
            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));
        }
    }
}
