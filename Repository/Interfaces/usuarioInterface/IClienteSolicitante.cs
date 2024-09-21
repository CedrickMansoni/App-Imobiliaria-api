using System;
using App_Imobiliaria_api.Models.mensagem;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IClienteSolicitante : IUsuario
{
    public Task<string> SolicitarImovel(SolicitacaoCliente solicitacao);
    public Task<List<SolicitacaoCliente>> VerSolicitacoesFeitas(int id);
    public Task<string> CancelarSolicitacao(int id);
}
