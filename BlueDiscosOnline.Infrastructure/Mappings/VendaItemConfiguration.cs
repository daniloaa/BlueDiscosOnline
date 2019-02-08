using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings
{
    public class VendaItemConfiguration : BaseEntityConfiguration<VendaItem>
    {
        public VendaItemConfiguration(EntityTypeBuilder<VendaItem> entityBuilder)
           : base(entityBuilder)
        {
            entityBuilder.Property(x => x.VendaId).HasColumnName("VendaId").IsRequired();
            entityBuilder.Property(x => x.AlbumId).HasColumnName("AlbumId").IsRequired();
        }

    }
}
