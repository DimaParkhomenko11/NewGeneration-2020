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
            ToTable("Media");
            HasKey(m => m.Id);

            Property(m => m.Id)
                .IsRequired();

            Property(m => m.PathToMedia)
                .IsRequired()
                .HasMaxLength(500);

            HasIndex(m => m.PathToMedia)
                .IsUnique();

            Property(m => m.isDeleted)
                .IsRequired();
        }
    }
}
