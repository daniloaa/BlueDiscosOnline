using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Models;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Services
{
    public interface IAlbumService : IBaseService<Album> 
    {
        RequestResult GetAlbumPaginated(AlbumModel albumModel);
    }
}
