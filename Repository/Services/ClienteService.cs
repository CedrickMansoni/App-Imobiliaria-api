using System;
using App_Imobiliaria_api.Models.imovel;
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

    public async Task<string> AdicionarFavorito(Favorito favorito)
    {
        var f = await context.TabelaFavorito.FirstOrDefaultAsync(f => f.CodigoImovel == favorito.CodigoImovel);

        if (f is not null) return "Não podemos adicionar este imóvel porque já faz parte da sua lista de favoritos";

        await context.TabelaFavorito.AddAsync(favorito);
        return await context.SaveChangesAsync() > 0 ? "Imóvel adicionado à sua lista de favoritos com sucesso" : "Erro: Não foi possível adicionar o imóvel à sua lista de favoritos, por favor tente novamente";
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

    public async Task<string> CancelarSolicitacao(int id)
    {
        var solicitacao = context.TabelaSolicitacaoCliente.FirstOrDefault(c => c.Id == id);
        if(solicitacao is not null)
        {
            context.TabelaSolicitacaoCliente.Remove(solicitacao);
        }
        return await context.SaveChangesAsync() > 0 ? 
        "Solicitação cancelada com sucesso" : 
        "Erro: Não conseguimos cancelar a solicitação, por favor tente novamente";
    }

    public async Task<List<ImovelModelResponse>> ListarFavoritos(int clienteId)
    {
        var cliente = await context.TabelaFavorito.Where(f => f.ClienteId == clienteId).OrderByDescending(x => x.Id).ToListAsync();

        var listaFavoritos = new List<ImovelModelResponse>();         
        
        if (cliente is not null)
        {
            var query = from imovel in context.TabelaImovel
            join localizacao in context.TabelaLocalizacao on imovel.IdLocalizacao equals localizacao.Id
            join rua in context.TabelaRua on localizacao.IdRua equals rua.Id
            join bairro in context.TabelaBairro on localizacao.IdBairro equals bairro.Id
            join municipio in context.TabelaMunicipio on localizacao.IdMunicipio equals municipio.Id
            join provincia in context.TabelaProvincia on localizacao.IdProvincia equals provincia.Id
            join pais in context.TabelaPais! on localizacao.IdPais equals pais.Id        
            join caracteristica in context.TabelaNaturezaImovel on imovel.IdNaturezaImovel equals caracteristica.Id
            join tipoImovel in context.TabelaTipoImovel on caracteristica.IdTipoImovel equals tipoImovel.Id
            join proprietario in context.TabelaClientesProprietarios on imovel.IdClienteProprietario equals proprietario.Id
            join corretor in context.TabelaFuncionarios on imovel.IdCorretor equals corretor.Id
            join tipoPublicacao in context.TabelaTipoPublicacao on imovel.TipoPublicidade equals tipoPublicacao.Id
        
            select new ImovelModelResponse
            {
                Imovel = imovel,
                Pais = pais,
                Provincia = provincia,
                Municipio = municipio,
                Bairro = bairro,
                Rua = rua,
                TipoImovel = tipoImovel,
                NaturezaImovel = caracteristica,
                ClienteProprietario = proprietario,
                CorretorImovel = corretor,
                Fotos = context.TabelaFoto.Where(f => f.IdImovel == imovel.Codigo).ToList(),
                FotoPrincipal = context.TabelaFoto.Where(f => f.IdImovel == imovel.Codigo).Select(f => f.Imagem).FirstOrDefault(),
                TipoPublicacao = tipoPublicacao
            };
            var resultado = await query.ToListAsync();

            foreach (var i in cliente)
            {
                if (resultado is not null)
                {
                    foreach (var item in resultado)
                    {                        
                        if (i.CodigoImovel == item.Imovel.Codigo)
                        {
                            item.ClienteProprietario.Senha = string.Empty;
                            item.CorretorImovel.Senha = string.Empty;

                            listaFavoritos.Add(item);
                            
                            break;
                        }                
                    }
                } else
                {
                    break;
                }               
            }
        }        
        return listaFavoritos;
    }

    public async Task<string> RemoverFavorito(Favorito favorito)
    {
        var f = await context.TabelaFavorito.FirstOrDefaultAsync(f => f.CodigoImovel == favorito.CodigoImovel && f.ClienteId == favorito.ClienteId);
        if (f == null) return "Não podemos remover este imóvel porque não consta na lista dos favoritos";

        context.TabelaFavorito.Remove(f);
        return await context.SaveChangesAsync() > 0 ? "O imóvel foi removido da sua lista de favoritos com sucesso" : "Erro: não foi possível remover o imǘel da sua lista de favoritos.";
    }

    public async Task<string> SolicitarImovel(SolicitacaoCliente solicitacao)
    {
        await context.TabelaSolicitacaoCliente.AddAsync(solicitacao);
        return await context.SaveChangesAsync() > 0 ? "Solicitação registrada com sucesso" : "Erro: Não foi possível registrar a solicitação. Por favor tente novamente";
    }

    public async Task<List<ImovelModelResponse>?> SolicitacoesFeitas(int id)
    {
        var solicitacoes = await context.TabelaSolicitacaoCliente.Where(f => f.IdClienteSolicitante == id).OrderByDescending(x => x.Id).ToListAsync();
                
        var listaNotificacoes = await context.TabelaNotificarCliente.ToListAsync();

        var listaSolicitacoes = new List<ImovelModelResponse>();         
        
        if (solicitacoes is not null)
        {
            var query = from imovel in context.TabelaImovel
            join localizacao in context.TabelaLocalizacao on imovel.IdLocalizacao equals localizacao.Id
            join rua in context.TabelaRua on localizacao.IdRua equals rua.Id
            join bairro in context.TabelaBairro on localizacao.IdBairro equals bairro.Id
            join municipio in context.TabelaMunicipio on localizacao.IdMunicipio equals municipio.Id
            join provincia in context.TabelaProvincia on localizacao.IdProvincia equals provincia.Id
            join pais in context.TabelaPais! on localizacao.IdPais equals pais.Id        
            join caracteristica in context.TabelaNaturezaImovel on imovel.IdNaturezaImovel equals caracteristica.Id
            join tipoImovel in context.TabelaTipoImovel on caracteristica.IdTipoImovel equals tipoImovel.Id
            join proprietario in context.TabelaClientesProprietarios on imovel.IdClienteProprietario equals proprietario.Id
            join corretor in context.TabelaFuncionarios on imovel.IdCorretor equals corretor.Id
            join tipoPublicacao in context.TabelaTipoPublicacao on imovel.TipoPublicidade equals tipoPublicacao.Id
        
            select new ImovelModelResponse
            {
                Imovel = imovel,
                Pais = pais,
                Provincia = provincia,
                Municipio = municipio,
                Bairro = bairro,
                Rua = rua,
                TipoImovel = tipoImovel,
                NaturezaImovel = caracteristica,
                ClienteProprietario = proprietario,
                CorretorImovel = corretor,
                Fotos = context.TabelaFoto.Where(f => f.IdImovel == imovel.Codigo).ToList(),
                FotoPrincipal = context.TabelaFoto.Where(f => f.IdImovel == imovel.Codigo).Select(f => f.Imagem).FirstOrDefault(),
                TipoPublicacao = tipoPublicacao
            };
            var resultado = await query.ToListAsync();

            foreach (var i in solicitacoes)
            {
                if (resultado is not null)
                {
                    foreach (var item in listaNotificacoes)
                    {                        
                        if (i.Id == item.IdSolicitacao)
                        {                            
                            foreach (var x in resultado)
                            {
                                if (x.Imovel.Codigo == item.IdPublicacao)
                                {
                                    x.ClienteProprietario.Senha = string.Empty;
                                    x.CorretorImovel.Senha = string.Empty;
                                    listaSolicitacoes.Add(x);
                                    break;
                                }
                            }
                        }                
                    }
                } else
                {
                    break;
                }               
            }
        }        
        return listaSolicitacoes;
    }
}
