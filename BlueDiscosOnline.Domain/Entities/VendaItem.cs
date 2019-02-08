using BlueDiscosOnline.Domain.Entities.Base;

namespace BlueDiscosOnline.Domain.Entities
{
    public class VendaItem : BaseEntity
    {
        public Venda Venda { get; set; }
        public long VendaId { get; set; }
        public Album Album { get; set; }
        public long AlbumId { get; set; }
        public decimal ValorCashBack { get; set; }
    }
}
