using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Entities.Base;

namespace BlueDiscosOnline.Domain.Entities
{
    public class Album : BaseEntity
    {
        public string Nome { get; set; }
        public string Artista { get; set; }
        public Genero Genero { get; set; }
        public long GeneroId { get; set; }
        public decimal Valor { get; set; }
    }
}
