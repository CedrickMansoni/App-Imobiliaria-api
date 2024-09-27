using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Repository.Interfaces.imovelInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Imobiliaria_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {
        private readonly IImovel imovel;
        public ImovelController(IImovel imovel)
        {
            this.imovel = imovel;
        }

        [HttpPost]
        [Route("/cadastrar/tipo/imovel")]
        public async Task<IActionResult> CadastrarTipoImovel([FromBody] TipoImovel tipo)
        {
            var response = await imovel.CadastrarTipoImovel(tipo);
            if (response is null)
            {
                return BadRequest("Erro: Não foi possível cadastrar o tipo de imóvel");
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("/listar/tipo/imovel")]
        public async Task<IActionResult> ListarTipoImovel()
        {
            return Ok(await imovel.ListarTipoImoveis());
        }

        [HttpPut]
        [Route("/editar/tipo/imovel")]
        public async Task<IActionResult> EditarTipoImovel([FromBody] TipoImovel tipo, int id)
        {
            var response = await imovel.EditarTipoImovel(tipo);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
