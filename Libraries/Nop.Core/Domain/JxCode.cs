using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain
{
    public class JxCode : BaseEntity
    {
        private ICollection<Student> students;

        public string Code { get; set; }

        public bool IsValid { get; set; }

        public int MapCustomCount { get; set; }

        public bool IsMapFull { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return students ?? (students = new List<Student>()); }
            protected set { students = value; }
        }
    }
}
