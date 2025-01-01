using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Imobiliaria_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteCompradorController : ControllerBase
    {
        private readonly IClienteSolicitante cliente;
        public ClienteCompradorController(IClienteSolicitante cliente)
        {
            this.cliente = cliente;
        }

        [HttpPost]
        [Route("/cadastrar/cliente/comprador")]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteSolicitante c)
        {
            var response = await cliente.CadastrarCliente(c);
            return response.Contains("Erro") ? BadRequest(response) : Ok(response);           
        }

        [HttpPost]
        [Route("/cadastrar/favorito")]
        public async Task<IActionResult> CadastrarFavorito([FromBody] Favorito favorito)
        {
            var response = await cliente.AdicionarFavorito(favorito);
            return response.Contains("Erro") ? BadRequest(response) : Ok(response);           
        }

        [HttpGet]
        [Route("/listar/favoritos/{id}")]
        public async Task<IActionResult> ListarFavoritos(int id)
        {
            return Ok(await cliente.ListarFavoritos(id));

        }

        [HttpPost]
        [Route("/remover/favorito")]
        public async Task<string> RemoverFavoritos(Favorito favorito) => await cliente.RemoverFavorito(favorito);

        [HttpPost]
        [Route("/cadastrar/solicitacao")]
        public async Task<string> CadastrarSolicitacao(SolicitacaoCliente solicitacao) => await cliente.SolicitarImovel(solicitacao);

        [HttpGet]
        [Route("/listar/notificacoes/{id}")]
        public async Task<IActionResult> ListarNotificacoes(int id) => Ok(await cliente.SolicitacoesFeitas(id));

        [HttpDelete]
        [Route("/cancelar/solicitacao/{id}")]
        public async Task<IActionResult> CancelarSolicitacao(int id) => Ok(await cliente.CancelarSolicitacao(id));
        
    }
}
