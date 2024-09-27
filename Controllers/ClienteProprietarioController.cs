using System;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.AspNetCore.Mvc;

namespace App_Imobiliaria_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClienteProprietarioController : ControllerBase
{
    private readonly IClienteProprietario proprietario;
    public ClienteProprietarioController(IClienteProprietario proprietario)
    {
        this.proprietario = proprietario;
    }

    [HttpPost]
    [Route("/cadastrar/proprietario")]
    public async Task<IActionResult> CadastrarClienteProprietario([FromBody] ClienteProprietario p)
    {
        if (p is null)
        {
            return BadRequest("Informe todos os dados do cliente");
        }
        var response = await proprietario.CriarConta(p);
        if (response.Contains("sucesso"))
        {
            return Ok(response); 
        }
        return BadRequest("Não foi possível criar a sua conta, por favor tente novamente.");
    }

}
