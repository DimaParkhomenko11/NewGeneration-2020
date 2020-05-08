using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models.ModelConfiguration
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("Roles").
                HasKey(p => p.Id).
                Property(p => p.Name).
                HasMaxLength(30).
                HasColumnType("varchar");
        }
    }
}
