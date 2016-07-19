using Nop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping
{
    public class JxCodeMap : EntityTypeConfiguration<JxCode>
    {
        public JxCodeMap()
        {
            this.ToTable("JxCode");
            this.Ignore(jx => jx.Id);
            this.HasKey(jx => jx.Code);
            this.Property(jx => jx.College).HasMaxLength(50);
            this.Property(jx => jx.Major).HasMaxLength(50);
            this.Property(jx => jx.Grade).HasMaxLength(50);
            this.Property(jx => jx.StudNo).HasMaxLength(50);
            this.HasMany(jc => jc.Students).WithOptional().HasForeignKey(s => s.JxCode);
        }
    }
}
