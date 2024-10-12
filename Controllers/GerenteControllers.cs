using System;
using App_Imobiliaria_api.Enumerables;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace App_Imobiliaria_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GerenteController : ControllerBase
{
    private readonly IGerente gerente;
    public GerenteController(IGerente gerente)
    {
        this.gerente = gerente;
    }

    
    [HttpPost]
    [Route("/cadastrar/pais")]
    public async Task<IActionResult> CadastrarPais([FromBody] Pais pais)
    {
        var response = await gerente.CadastrarPais(pais);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/provincia")]
    public async Task<IActionResult> CadastrarProvincia([FromBody] Provincia provincia)
    {
        var response = await gerente.CadastrarProvincia(provincia);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/municipio")]
    public async Task<IActionResult> CadastrarMunicipio([FromBody] Municipio municipio)
    {
        var response = await gerente.CadastrarMunicipio(municipio);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/bairro")]
    public async Task<IActionResult> CadastrarBairro([FromBody] Bairro bairro)
    {
        var response = await gerente.CadastrarBairro(bairro);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/rua")]
    public async Task<IActionResult> CadastrarRua([FromBody] Rua rua)
    {
        var response = await gerente.CadastrarRua(rua);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [HttpGet]
    [Route("/listar/zonas")]
    public async Task<IActionResult> ListarZonas()
    {
        var response = await gerente.ListarPaises();
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/funcionario")]
    public async Task<IActionResult> CadastrarFuncionario([FromBody] Funcionario funcionario)
    {
        var response = await gerente.CadastrarCorretor(funcionario);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("/listar/funcionarios")]
    public async Task<IActionResult> ListarFuncionarios()
    {
        var response = await gerente.ListarFuncionarios();
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [Route("/cadastrar/token")]
    public async Task<IActionResult> CadastrarToken([FromBody] Token token)
    {
        var response = await gerente.RenovarToken(token);
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    [HttpGet]
    [Route("/pegar/token")]
    public async Task<IActionResult> PegarToken()
    {
        var response = await gerente.PegarToken();
        if (response is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
