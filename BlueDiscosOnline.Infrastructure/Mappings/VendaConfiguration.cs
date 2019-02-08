using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings
{
    public class VendaConfiguration : BaseEntityConfiguration<Venda>
    {
        public VendaConfiguration(EntityTypeBuilder<Venda> entityBuilder)
           : base(entityBuilder)
        {
            entityBuilder.Property(x => x.ValorCashBackTotal).HasColumnName("ValorCashBackTotal");

        }

    }
}
