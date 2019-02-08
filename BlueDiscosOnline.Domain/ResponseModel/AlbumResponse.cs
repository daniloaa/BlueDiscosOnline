using BlueDiscosOnline.Domain.Entities;
using System.Collections.Generic;

namespace BlueDiscosOnline.Domain.ResponseModel
{
    public class AlbumResponse
    {
        public int Quantidade { get; set; }
        public List<Album> Albuns { get; set; }
    }
}
