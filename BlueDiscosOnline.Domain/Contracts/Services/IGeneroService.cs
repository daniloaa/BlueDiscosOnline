using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Services
{
    public interface IGeneroService : IBaseService<Genero>
    {        
        List<Genero> GetAllGenero();
        Task<RequestResult> PostGenero(List<Genero> generoList);
    }
}
