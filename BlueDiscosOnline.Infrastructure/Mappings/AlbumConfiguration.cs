using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings
{
    public class AlbumConfiguration : BaseEntityConfiguration<Album>
    {
        public AlbumConfiguration(EntityTypeBuilder<Album> entityBuilder)
           : base(entityBuilder)
        {
            entityBuilder.Property(x => x.Nome).HasColumnName("Nome").IsRequired();

        }

    }
}
