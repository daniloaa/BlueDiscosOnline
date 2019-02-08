using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace BlueDiscosOnline.API.Controllers.Base
{
    public abstract class BaseController<TEntity> : Controller
        where TEntity : BaseEntity
    {
        private readonly IStringLocalizer Localizer;
        private readonly IBaseService<TEntity> Service;

        public BaseController(IBaseService<TEntity> service, IStringLocalizer<LanguageLocalizer> localizer)
        {
            Service = service;
            Localizer = localizer;
        }
        
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {

                return Ok(await Service.Get(id));
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(Localizer["UnexpectedError"]))));
            }
        }
    }
}
