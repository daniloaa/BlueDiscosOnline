using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueDiscosOnline.Domain.Entities
{
    public class GeneroPercentualDia : BaseEntity
    {      
        public Genero Genero { get; set; }
        public long GeneroId { get; set; }
        public int DiaSemana { get; set; }
        public int Percentual { get; set; }
    }
}
