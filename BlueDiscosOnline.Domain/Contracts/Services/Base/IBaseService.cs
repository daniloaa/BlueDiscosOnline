using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Domain.Entities.Base;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Services.Base
{
    public interface IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        //Métodos comuns a todos os services
        Task<RequestResult> Get(long id);
    }
}
