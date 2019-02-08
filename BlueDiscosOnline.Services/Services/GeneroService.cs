using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Services.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Services.Services
{
    public class GeneroService : BaseService<Genero>, IGeneroService
    {
        public GeneroService( IUnitOfWorkFactory unitOfWorkFactory,
                              IStringLocalizer<LanguageLocalizer> localizer
                             )
            : base(unitOfWorkFactory, localizer)
        {
            
        }

        public List<Genero> GetAllGenero() {

            var generoList = new List<Genero>();

            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    generoList = unitOfWork.Repository.Get<Genero>(x => x.Id > 0).ToList();
                    return generoList;
                }
            }
            catch (Exception)
            {
                return generoList;
            }
        }

        public async Task<RequestResult> PostGenero(List<Genero> generoList)
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    foreach (var genero in generoList)
                    {
                       
                        unitOfWork.Repository.Add(genero);                            
                                            
                    }

                    await unitOfWork.Commit();
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
