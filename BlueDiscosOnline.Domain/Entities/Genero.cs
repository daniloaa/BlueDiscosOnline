using BlueDiscosOnline.Domain.Entities.Base;
using System.Collections.Generic;

namespace BlueDiscosOnline.Domain.Entities
{
    public class Genero : BaseEntity
    {
        public string Descricao { get; set; }
        public List<GeneroPercentualDia> PercentualDia { get; set; }
    }
}
