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
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRole ?? (_customerRole = new List<CustomerRole>()); }
            protected set { _customerRole = value; }
        }
    }
}
