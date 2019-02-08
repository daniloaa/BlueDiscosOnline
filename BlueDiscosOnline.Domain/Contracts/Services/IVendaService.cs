using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Services
{
    public interface IVendaService : IBaseService<Venda> 
    {
        Task<RequestResult> NovaVenda(List<int> vendaItensId);
        Task<RequestResult> GetVendaPaginated(VendaModel vendaModel);
    }
}
