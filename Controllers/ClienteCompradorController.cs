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
    }
}
