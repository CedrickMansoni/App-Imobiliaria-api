using System;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IClienteProprietario 
{
    public Task<string> SolicitarPublicacaoImovel(Imovel imovel);
    public Task<List<Imovel>> VerSolicitaSolicitacoes(int id);
    public Task<string> CriarConta(ClienteProprietario proprietario);
    public Task<ClienteProprietario?> GetProprietario(string telefone);
}
