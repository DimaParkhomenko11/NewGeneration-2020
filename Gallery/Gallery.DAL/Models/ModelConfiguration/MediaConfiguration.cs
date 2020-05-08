using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models.ModelConfiguration
{
    public class MediaConfiguration : EntityTypeConfiguration<Media>
    {
        public MediaConfiguration()
        {
            ToTable("Media").
                HasKey(p => p.Id).
                Property(p => p.PathToMedia).
                HasMaxLength(25).
                HasColumnType("varchar");
        }
    }
}
