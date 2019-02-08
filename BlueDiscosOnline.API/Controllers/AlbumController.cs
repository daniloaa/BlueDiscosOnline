using BlueDiscosOnline.API.Controllers.Base;
using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace BlueDiscosOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumController : BaseController<Album>
    {
        private readonly IAlbumService _albumService;
        private readonly IStringLocalizer _localizer;

        public AlbumController(IAlbumService albumService, IStringLocalizer<LanguageLocalizer> localizer)
             : base(albumService, localizer)
        {
            _albumService = albumService;
            _localizer = localizer;
        }

        /*
            Método que retorna albuns cadastrados com informações de paginação
            Entrada: Genero, Skip, Take
            Retorno: Quantidade total de albuns que atendem o filtro para construir a numeração das paginas
                     Albuns cadastradas realizando o Skip e Take para atender a paginação.
        */
        [HttpGet("GetAlbumPaginated"), Route("GetAlbumPaginated")]
        public IActionResult GetAlbumPaginated(AlbumModel albumModel)
        {      
            var result = new RequestResult(StatusResult.Success);
            try
            {
                result = _albumService.GetAlbumPaginated(albumModel);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"]))));  
            }
        }
    }
}
