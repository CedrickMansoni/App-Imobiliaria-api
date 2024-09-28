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
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest("Erro: Não foi possível cadastrar o tipo de imóvel");            
        }

        [HttpGet]
        [Route("/listar/tipo/imovel")]
        public async Task<IActionResult> ListarTipoImovel()
        {
            return Ok(await imovel.ListarTipoImoveis());
        }

        [HttpPut]
        [Route("/editar/tipo/imovel")]
        public async Task<IActionResult> EditarTipoImovel([FromBody] TipoImovel tipo)
        {
            var response = await imovel.EditarTipoImovel(tipo);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("/eliminar/tipo/imovel/{id}")]
        public async Task<IActionResult> EliminarTipoImovel(int id)
        {
            var response = await imovel.EliminarTipoImovel(id);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("/listar/paises")]
        public async Task<IActionResult> ListarPaisesImovel()
        {
            return Ok(await imovel.ConsultarPaisImovel());
        }

        [HttpGet]
        [Route("/listar/provincias/{id}")]
        public async Task<IActionResult> ListarProvinciasImovel(int id)
        {
            return Ok(await imovel.ConsultarProviciaImovel(id));
        }

        [HttpGet]
        [Route("/listar/municipios/{id}")]
        public async Task<IActionResult> ListarMunicipiosImovel(int id)
        {
            return Ok(await imovel.ConsultarMunicipioImovel(id));
        }

        [HttpGet]
        [Route("/listar/bairros/{id}")]
        public async Task<IActionResult> ListarBairrosImovel(int id)
        {
            return Ok(await imovel.ConsultarBairroImovel(id));
        }

        [HttpGet]
        [Route("/listar/ruas/{id}")]
        public async Task<IActionResult> ListarRuasImovel(int id)
        {
            return Ok(await imovel.ConsultarRuaImovel(id));
        }
    }
}
