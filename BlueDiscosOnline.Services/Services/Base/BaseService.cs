using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Domain.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Services.Services.Base
{
    public class BaseService<T> : IBaseService<T>
         where T : BaseEntity
    {
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }
        public readonly IStringLocalizer _localizer;

        public BaseService( IUnitOfWorkFactory unitOfWorkFactory,
                            IStringLocalizer<LanguageLocalizer> localizer)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            _localizer = localizer;
        }

        public async Task<RequestResult> Save(T entity)
        {
            var result = new RequestResult(StatusResult.Success);

            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {     
                    if (entity.Id == 0)
                    {                      
                        unitOfWork.Repository.Add(entity);
                    }
                    else
                    {
                        await unitOfWork.Repository.Edit(entity.Id, entity);
                    }
                    
                    await unitOfWork.Commit();

                    result.Data = entity;
                    result.Messages.Add(new Message(_localizer["EntitySaveSuccess"]));
                }
            }
            catch (Exception)
            {
                result.Status = StatusResult.Danger;
                result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"])));
            }

            return result;
        }

        [HttpGet("{id}")]
        public virtual async Task<RequestResult> Get(long id)
        {
            var result = new RequestResult(StatusResult.Success);

            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    var obj = await unitOfWork.Repository
                                        .First<T>(x => x.Id == id);

                    result.Data = await BeforeReturnGet(obj);

                    if (result.Data == null)
                    {
                        result.Status = StatusResult.Warning;
                        result.Messages.Add(new Message(_localizer["EntityNotFound"]));
                    }
                }
            }
            catch (Exception)
            {
                result.Status = StatusResult.Danger;
                result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"])));
            }

            return result;
        }

        protected virtual Task<T> BeforeReturnGet(T entity)
        {
            return Task.FromResult(entity);
        }
    }
}
