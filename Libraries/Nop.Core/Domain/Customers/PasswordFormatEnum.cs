using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public enum PasswordFormatEnum
    {
        Clear = 0,
        Hashed = 1,
        Encrypted = 2
    }
}
