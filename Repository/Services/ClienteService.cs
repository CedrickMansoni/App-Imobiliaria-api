using System;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.Repository.Services;

public class ClienteService : IClienteSolicitante
{
    private readonly ImobContext.ImobContext context;
    public ClienteService(ImobContext.ImobContext context)
    {
        this.context = context;
    }
    public async Task<string> CadastrarCliente(ClienteSolicitante cliente)
    {
        var c = await context.TabelaClientesSolicitantes.FirstOrDefaultAsync(c => c.Telefone == cliente.Telefone);
        if(c is not null)
        {
            return "Este telefone já pertence a uma conta cadastrada no aplicativo";
        }
        await context.TabelaClientesSolicitantes.AddAsync(cliente);
        return await context.SaveChangesAsync() > 0 ? "Cliente cadastrado com sucesso" : "Erro:\n Não foi possível criar a conta do cliente, por favor tente novamente";
    }

    public Task<string> CancelarSolicitacao(int id)
    {
        throw new NotImplementedException();
    }

    public Task<string> SolicitarImovel(SolicitacaoCliente solicitacao)
    {
        throw new NotImplementedException();
    }

    public Task<List<SolicitacaoCliente>> VerSolicitacoesFeitas(int id)
    {
        throw new NotImplementedException();
    }
}
