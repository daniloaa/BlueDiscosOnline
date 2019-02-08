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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueDiscosOnline.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VendaController : BaseController<Venda>
    {
        private readonly IVendaService _vendaService;
        private readonly IStringLocalizer _localizer;

        public VendaController(IVendaService vendaService, IStringLocalizer<LanguageLocalizer> localizer)
             : base(vendaService, localizer)
        {
            _vendaService = vendaService;
            _localizer = localizer;
        }

        /*
            Método de cadastro de novas vendas.
            Entrada: Lista de inteiros contendo identificadores do(s) album(s) que estão sendo vendidos.
            Retorno: Informações da venda como Data da Venda, Identificador, Valor total de cashback.
                     Uma lista de albuns da venda com seus valores de cashback
        */

        [HttpPost("NovaVenda"), Route("NovaVenda")]
        public async Task<IActionResult> NovaVenda([FromBody]List<int> vendaItensId)
        {            
            var result = new RequestResult(StatusResult.Success);
            try
            {
                result = await _vendaService.NovaVenda(vendaItensId);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"]))));
            }
        }
        /*
            Método que retorna vendas cadastradas com informações de paginação
            Entrada: DataInicial, DataFinal, Skip, Take
            Retorno: Quantidade total de vendas que atendem o filtro para construir a numeração das paginas
                     Vendas cadastradas realizando o Skip e Take para atender a paginação.
        */
        [HttpGet("GetVendaPaginated"), Route("GetVendaPaginated")]
        public async Task<ActionResult> GetVendaPaginated(VendaModel vendaModel)
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                result = await _vendaService.GetVendaPaginated(vendaModel);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"]))));
            }
        }
    }
}
