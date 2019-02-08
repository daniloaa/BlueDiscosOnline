using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace BlueDiscosOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpotifyController : Controller
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IStringLocalizer _localizer;

        public SpotifyController(ISpotifyService spotifyService, IStringLocalizer<LanguageLocalizer> localizer)
        {
            _spotifyService = spotifyService;
            _localizer = localizer;
        }
        /*
            Método que consome API do Spotify buscando albuns por gênero e cadastrando no banco
            Entrada: - 
            Retorno: Lista de albuns cadastrados no banco de dados.
        */
        [HttpGet("GetDataSpotify"), Route("GetDataSpotify")]
        public async Task<IActionResult> GetDataSpotify()
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                result = await _spotifyService.GetDataSpotify();
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"])))); 
            }
        }
    }
}
