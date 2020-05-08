using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.ModelConfiguration
{
    public class MediaTypeConfiguration : EntityTypeConfiguration<MediaType>
    {
        public MediaTypeConfiguration()
        {
            ToTable("MediaType").
                HasKey(p => p.Id).
                Property(p => p.Type).HasMaxLength(25).
                HasColumnType("varchar");

           
                HasMany(p => p.Media)
                .WithRequired(p => p.MediaType);
        }
    }
}
