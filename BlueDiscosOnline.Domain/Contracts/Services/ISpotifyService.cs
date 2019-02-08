using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Domain.ResponseModel;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Services
{
    public interface ISpotifyService
    {
        Task<RequestResult> GetDataSpotify();
    }
}
