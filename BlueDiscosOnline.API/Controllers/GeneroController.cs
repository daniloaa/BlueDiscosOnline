using BlueDiscosOnline.API.Controllers.Base;
using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueDiscosOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GeneroController : BaseController<Genero>
    {
        private readonly IGeneroService _generoService;
        private readonly IStringLocalizer _localizer;

        public GeneroController(IGeneroService generoService, IStringLocalizer<LanguageLocalizer> localizer) 
            :base(generoService, localizer)
        {
            _generoService = generoService;
            _localizer = localizer;
        }

        /*
            Método que cadastra gêneros
            Entrada: Descrição e lista dos percentuais de cashback por dia da semana
            Retorno: Container com status da operação
        */
        [HttpPost("PostGenero"), Route("PostGenero")]
        public async Task<IActionResult> PostGenero([FromBody]List<Genero> generoList)
        {            
            var result = new RequestResult(StatusResult.Success);
            try
            {
                result = await _generoService.PostGenero(generoList);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"]))));
            }
        }
    }
}
