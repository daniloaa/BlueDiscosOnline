using BlueDiscosOnline.Domain.Entities;
using System.Collections.Generic;

namespace BlueDiscosOnline.Domain.ResponseModel
{
    public class VendaResponse
    {
        public int Quantidade { get; set; }
        public List<Venda> Venda { get; set; }
    }
}
