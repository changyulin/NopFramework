﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public int? Age { get; set; }

        public string JxCode { get; set; }
    }
}
