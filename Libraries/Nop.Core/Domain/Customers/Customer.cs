using Nop.Core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public class Customer : BaseEntity
    {
        private ICollection<CustomerRole> _customerRole;

        public Guid CustomerGuid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public PasswordFormatEnum PasswordFormat
        {
            get { return (PasswordFormatEnum)PasswordFormatId; }
            set { this.PasswordFormatId = (int)value; }
        }
        public string PasswordSalt { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime? LastActivityDateUtc { get; set; }
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRole ?? (_customerRole = new List<CustomerRole>()); }
            protected set { _customerRole = value; }
        }

        /// <summary>
        /// Gets a value indicating whether customer is in a certain customer role
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="customerRoleSystemName">Customer role system name</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        public bool IsInCustomerRole(
            string customerRoleSystemName, bool onlyActiveCustomerRoles = true)
        {
            if (String.IsNullOrEmpty(customerRoleSystemName))
                throw new ArgumentNullException("customerRoleSystemName");

            var result = this.CustomerRoles
                .FirstOrDefault(cr => (!onlyActiveCustomerRoles || cr.Active) && (cr.SystemName == customerRoleSystemName)) != null;
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether customer is registered
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="onlyActiveCustomerRoles">A value indicating whether we should look only in active customer roles</param>
        /// <returns>Result</returns>
        public bool IsRegistered(bool onlyActiveCustomerRoles = true)
        {
            return IsInCustomerRole(SystemCustomerRoleNames.Registered, onlyActiveCustomerRoles);
        }
    }
}
