using System;
using App_Imobiliaria_api.Models.imovel;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IClienteProprietario : IUsuario
{
    public Task<string> SolicitarPublicacaoImovel(Imovel imovel);
    public Task<List<Imovel>> VerSolicitaSolicitacoes(int id);
}
