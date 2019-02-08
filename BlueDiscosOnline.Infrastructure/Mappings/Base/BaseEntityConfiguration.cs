using BlueDiscosOnline.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueDiscosOnline.Infrastructure.Mappings.Base
{
    public abstract class BaseEntityConfiguration<T>
       where T : BaseEntity
    {
        public BaseEntityConfiguration(EntityTypeBuilder<T> entityBuilder)
        {
            //Colunas comuns a todas as entidades/tabelas.
            entityBuilder.HasKey(t => t.Id);
        }
    }
}
