using System;
using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace App_Imobiliaria_api.Repository.Services;

public class UsuarioService : IUsuario
{
    private readonly ImobContext.ImobContext context;
    public UsuarioService(ImobContext.ImobContext context )
    {
        this.context = context;
    }


    public Task<Usuario> EditarPerfil(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<ModelResponse<Usuario>?> FazerLogin(ModelResponse<Usuario> usuario)
    {
        var modelResponse = new ModelResponse<Usuario>();
        if (usuario.Dados is not null)
        {
            var response = await context.TabelaFuncionarios.FirstOrDefaultAsync(f => f.Telefone == usuario.Dados.Telefone && f.Senha == usuario.Dados.Senha);
            if (response is not null)
            {
                response.Senha = string.Empty;
                modelResponse.Dados = response;
                modelResponse.Mensagem = "Sucesso";
                modelResponse.UserType = response.Nivel; 
                modelResponse.Estado = response.Estado;                   
            }else
            {
                modelResponse.Mensagem = "Telefone ou senha invalida";
            }       
        }        
        return modelResponse;
    }

    public Task<Usuario> VerPerfil(int id)
    {
        throw new NotImplementedException();
    }
}
