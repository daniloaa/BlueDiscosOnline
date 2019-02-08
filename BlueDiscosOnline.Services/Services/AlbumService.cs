using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Models;
using BlueDiscosOnline.Domain.ResponseModel;
using BlueDiscosOnline.Services.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace BlueDiscosOnline.Services.Services
{
    public class AlbumService : BaseService<Album>, IAlbumService
    {
        public AlbumService( IUnitOfWorkFactory unitOfWorkFactory,
                              IStringLocalizer<LanguageLocalizer> localizer
                             )
            : base(unitOfWorkFactory, localizer)
        {
            
        }       

        public RequestResult GetAlbumPaginated(AlbumModel albumModel)
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    var albumQuantidade = unitOfWork.Repository.Count<Album>(x => x.GeneroId == albumModel.GeneroId).Result;

                    var albumList = unitOfWork.Repository.Get<Album>(x => x.GeneroId == albumModel.GeneroId).OrderBy(x => x.Nome)
                                                                                                            .Skip(albumModel.Skip)
                                                                                                            .Take(albumModel.Take).ToList();
                    var resultPaginated = new AlbumResponse { Albuns = albumList, Quantidade = albumQuantidade };

                    result.Data = resultPaginated;
                }
               
            }
            catch (Exception ex)
            {
                result.Status = StatusResult.Danger;
                result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"], ex.Message)));
            }

            return result;
        }
    }
}
