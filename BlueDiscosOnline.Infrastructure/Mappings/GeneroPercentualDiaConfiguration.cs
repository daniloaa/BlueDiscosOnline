using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings
{
    public class GeneroPercentualDiaConfiguration : BaseEntityConfiguration<GeneroPercentualDia>
    {
        public GeneroPercentualDiaConfiguration(EntityTypeBuilder<GeneroPercentualDia> entityBuilder)
           : base(entityBuilder)
        {
            entityBuilder.Property(x => x.DiaSemana).HasColumnName("DiaSemana").IsRequired();
        }

    }
}
