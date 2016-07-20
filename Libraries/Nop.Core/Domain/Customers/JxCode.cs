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

        public string College { get; set; }

        public string Major { get; set; }

        public string Grade { get; set; }

        public string StudNo { get; set; }

        public bool IsValid { get; set; }

        public bool IsUsed { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return students ?? (students = new List<Student>()); }
            protected set { students = value; }
        }
    }
}
