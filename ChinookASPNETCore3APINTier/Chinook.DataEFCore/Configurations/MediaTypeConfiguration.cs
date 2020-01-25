using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chinook.DataEFCore.Configurations
{
    public class MediaTypeConfiguration
    {
        public MediaTypeConfiguration(EntityTypeBuilder<MediaType> entity)
        {
            entity.HasIndex(e => e.MediaTypeId)
                .HasName("IPK_MediaType");

            entity.Property(e => e.Name).HasMaxLength(120);
        }
    }
}