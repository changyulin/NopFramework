using Nop.Core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public class CustomerRole : BaseEntity
    {
        private ICollection<PermissionRecord> _permissionRecords;

        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool Active { get; set; }
        public bool IsSystemRole { get; set; }
        public virtual ICollection<PermissionRecord> PermissionRecords
        {
            get { return _permissionRecords ?? (_permissionRecords = new List<PermissionRecord>()); }
            protected set { _permissionRecords = value; }
        }
    }
}
