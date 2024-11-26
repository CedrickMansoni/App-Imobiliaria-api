using System;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IClienteSolicitante
{
    public Task<string> CadastrarCliente(ClienteSolicitante cliente);
    public Task<string> SolicitarImovel(SolicitacaoCliente solicitacao);
    public Task<List<SolicitacaoCliente>> VerSolicitacoesFeitas(int id);
    public Task<string> CancelarSolicitacao(int id);
    public Task<string> AdicionarFavorito(Favorito favorito);
    public Task<string> RemoverFavorito(Favorito favorito);
    public Task<List<ImovelModelResponse>> ListarFavoritos(int clienteId);
}
