using System;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.Repository.Services;

public class ProprietarioService : IClienteProprietario
{
    private readonly ImobContext.ImobContext context;
    public ProprietarioService(ImobContext.ImobContext context)
    {
        this.context = context;
    }
    public async Task<string> CriarConta(ClienteProprietario proprietario)
    {
        var p = await context.TabelaClientesProprietarios.FirstOrDefaultAsync(p => p.Telefone == proprietario.Telefone);
        if (p is not null)
        {
            return "Já existe uma conta com este número de telefone";
        }
        await context.TabelaClientesProprietarios.AddAsync(proprietario);
        if (await context.SaveChangesAsync() == 1)
        {
            return "Conta criada com sucesso";
        }
        return $"Erro: Não foi possível criar conta para o cliente {proprietario.Nome}, por favor tente novamente.";
    }

    public Task<string> SolicitarPublicacaoImovel(Imovel imovel)
    {
        throw new NotImplementedException();
    }

    public Task<List<Imovel>> VerSolicitaSolicitacoes(int id)
    {
        throw new NotImplementedException();
    }
}
