using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models.ModelConfiguration
{
    public class TemporaryMediaConfiguration : EntityTypeConfiguration<TemporaryMedia>
    {
        public TemporaryMediaConfiguration()
        {
            ToTable("TemporaryMedia")
                .HasKey(p => p.Id).
                Property(a => a.IsSuccess).
                IsRequired();
            
            Property(m => m.UniqueIdentificator)
                .IsRequired();
        }
    }
}
