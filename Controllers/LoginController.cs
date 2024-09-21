using System;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.AspNetCore.Mvc;

namespace App_Imobiliaria_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUsuario usuario;
    public LoginController(IUsuario usuario)
    {
        this.usuario = usuario;
    }

    [HttpPost]
    [Route("/funcionario")]
    public async Task<IActionResult> FazerLogin([FromBody] ModelResponse<Usuario> modelResponse){
        if (modelResponse.Dados is null)
        {
            return BadRequest("Dados incompletos");
        }
        var response = await usuario.FazerLogin(modelResponse);
        return Ok(response);
    }
}
