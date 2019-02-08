using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings
{
    public class GeneroConfiguration : BaseEntityConfiguration<Genero>
    {
        public GeneroConfiguration(EntityTypeBuilder<Genero> entityBuilder)
           : base(entityBuilder)
        {
            entityBuilder.Property(x => x.Descricao).HasColumnName("Descricao").IsRequired();
            entityBuilder.HasMany(c => c.PercentualDia).WithOne(e => e.Genero);
        }

    }
}
