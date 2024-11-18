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


    public async Task<bool> EditarPerfil(string avatar, int id)
    {
        var perfilDB = await context.TabelaFuncionarios.FirstOrDefaultAsync(f => f.Id == id);
        if (perfilDB is not null)
        {
            perfilDB.Avatar = avatar;
            
            if (await context.SaveChangesAsync() > 0)
            {
                return true;
            }            
        }
        return false;
    }

    public async Task<bool> EditarSenhafuncionario(Funcionario funcionario)
    {
        var f = await context.TabelaFuncionarios.FirstOrDefaultAsync(f => f.Id == funcionario.Id);
        if (f is not null)
        {
            f.Senha = funcionario.Senha;
            context.Entry(f).Property(x => x.Senha).IsModified = true;
            return await context.SaveChangesAsync() > 0 ? true: false;        
        }
        return false;
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
                return modelResponse;                 
            }

            
            var clienteSolicitante = await context.TabelaClientesSolicitantes.FirstOrDefaultAsync(c => c.Telefone == usuario.Dados.Telefone && c.Senha == usuario.Dados.Senha);
            if (clienteSolicitante is not null)
            {
                clienteSolicitante.Senha = string.Empty;
                modelResponse.Dados = clienteSolicitante;
                modelResponse.Estado = clienteSolicitante.Estado;
                modelResponse.Mensagem = "Sucesso";
                modelResponse.UserType = "Comprador"; 
                return modelResponse;                 
            }          
        } 

        modelResponse.Mensagem = "Telefone ou senha invalida";       
        return modelResponse;
    }

    public async Task<ModelResponse<Funcionario>> VerPerfil(int id)
    {
        var modeleResponse = new ModelResponse<Funcionario>();
        
        var lista = await context.TabelaFuncionarios.Where(f => f.Id == id).ToListAsync();
        
        var model = new ModelResponse<Funcionario>();

        foreach (var item in lista)
        {
            
            item.Senha = string.Empty;
            model.Dados = item;
            if (!string.IsNullOrEmpty(item.Avatar))
            {
                model.Avatar = item.Avatar;
            }            
            var provincia = await context.TabelaProvincia.FindAsync(item.IdProvincia);
            model.UserType = item.Nivel;
            model.Estado = item.Estado;
            if(provincia is not null)
            {
                model.Mensagem = provincia.NomeProvincia; 
            }                       
        }
        modeleResponse = model;
        
        return modeleResponse;
    }
}
