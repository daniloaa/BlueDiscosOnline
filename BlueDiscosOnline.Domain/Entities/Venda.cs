using BlueDiscosOnline.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace BlueDiscosOnline.Domain.Entities
{
    public class Venda : BaseEntity
    {
        public decimal ValorCashBackTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public List<VendaItem> Itens { get; set; } = new List<VendaItem>();
    }
}
