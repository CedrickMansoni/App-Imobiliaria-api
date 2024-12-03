using System;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.mensagem;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface ICorretor 
{
    Task<string> PublicacarImovel(Publicacao publicacao);
    Task<List<Imovel>> VerPedidosPublicaçãoImovel();
    Task<string> ValidarPedidoPublicacaoImovel(Publicacao publicacaoValidada);
    Task<string> RejeitarPedidoPublicacaoImovel(Publicacao publicacaoRejeitada);
    Task<string> NotificarProprietarioImovel(NotificarProprietario notificarProprietario);
    Task NotificarClientes(NotificarCliente notificarCliente);
    Task<List<Imovel>> VerSolicitaSolicitacoes();
}
