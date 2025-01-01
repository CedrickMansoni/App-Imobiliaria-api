using System;
using System.Transactions;
using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Repository.Interfaces.imovelInterface;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.Repository.Services;

public class ImovelService : IImovel
{
    private readonly ImobContext.ImobContext context;
    private static readonly Random random = new Random();

    private readonly IGerente gerente;

    public ImovelService(ImobContext.ImobContext context, IGerente gerente)
    {
        this.context = context;
        this.gerente = gerente;
    }

    public async Task<string> CadastrarImovel(ImovelModelDTO imovel)
    {
        using (var transacao = context.Database.BeginTransaction())
        {
            

            foreach (var item in imovel.Rua)
            {
                imovel.IdRua = item.Id;
            }
            foreach (var item in imovel.Bairro)
            {
                imovel.IdBairro = item.Id;
            }
            foreach (var item in imovel.Municipio)
            {
                imovel.IdMunicipio = item.Id;
            }
            foreach (var item in imovel.Provincia)
            {
                imovel.IdProvincia = item.Id;
            }
            foreach (var item in imovel.Pais)
            {
                imovel.IdPais = item.Id;
            }

            var localizacao = new Localizacao()
            {
                IdRua = imovel.IdRua,
                IdBairro = imovel.IdBairro,
                IdMunicipio = imovel.IdMunicipio,
                IdProvincia = imovel.IdProvincia,
                IdPais = imovel.IdPais
            };
            await context.TabelaLocalizacao.AddAsync(localizacao);
            if (await context.SaveChangesAsync() == 1)
            {
                var l = await context.TabelaLocalizacao.OrderByDescending(l => l.Id).FirstOrDefaultAsync();
                if(l is not null)
                imovel.Imovel.IdLocalizacao = l.Id;
            }

            var caracteristica = new NaturezaImovel();
            if(imovel.NaturezaImovel is not null)
            foreach (var item in imovel.NaturezaImovel)
            {
                caracteristica.Caracteristica = item.Caracteristica;
                caracteristica.Descricao = item.Descricao;
                caracteristica.IdTipoImovel = item.IdTipoImovel;
            }

            await context.TabelaNaturezaImovel.AddAsync(caracteristica);
            if (await context.SaveChangesAsync() == 1)
            {
                var natureza = await context.TabelaNaturezaImovel.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
                if(natureza is not null)
                imovel.Imovel.IdNaturezaImovel = natureza.Id;
            }

            imovel.Imovel.Codigo += GerarNumeroAleatorio();

            var imovelDb = new Imovel()
            {
                Codigo = imovel.Imovel.Codigo,
                IdClienteProprietario = imovel.ClienteProprietario.Id,
                Descricao = imovel.Imovel.Descricao,
                DataSolicitacao = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Utc),
                Estado = "Pendente",
                TipoPublicidade = imovel.Imovel.TipoPublicidade,
                Preco = imovel.Imovel.Preco,
                IdNaturezaImovel = imovel.Imovel.IdNaturezaImovel,
                IdLocalizacao = imovel.Imovel.IdLocalizacao,
                IdCorretor = imovel.IdCorretor
            };

            await context.TabelaImovel.AddAsync(imovelDb);
            if(await context.SaveChangesAsync() == 1)
            {
                await transacao.CommitAsync();
                return $"{imovelDb.Codigo}";
            }else
            {
                await transacao.RollbackAsync();
                return "Erro: Não foi possível cadastrar o imóvel";
            }
        }
    }

    public static int GerarNumeroAleatorio()
    {
        return random.Next(1000, 9999);
    }

    public Task<NaturezaImovel> CadastrarNaturezaImove(NaturezaImovel natureza)
    {
        throw new NotImplementedException();
    }

    public async Task<string> CadastrarTipoImovel(TipoImovel tipo)
    {
        var tipoDb = await context.TabelaTipoImovel.FirstOrDefaultAsync(t => t.TipoImovelDesc == tipo.TipoImovelDesc);
        if (tipoDb is null)
        {
            tipo.Estado = true;
            await context.TabelaTipoImovel.AddAsync(tipo);
            if (await context.SaveChangesAsync() == 1)
            {
                return "Tipo de imóvel foi cadastrado com sucesso";
            }
        }
        else
        {            
            if (tipoDb.Estado)
            {
                return "Erro: Não foi possível cadastrar este tipo de imóvel porque já existe no banco de dados.";
            }else
            {
                var r = EliminarTipoImovel(tipoDb.Id, true);
                return "OBS:\nEste tipo de imóvel foi recuperado da lixeira com sucesso.";
            }            
        }
        return "Erro: Problema de comunicação com o servidor";
    }

    public async Task<ImovelModelDTO> ConsultarBairroImovel(int id)
    {
        var response = new ImovelModelDTO(); 
        var listaBairros = await context.TabelaBairro.Where(p => p.IdMunicipio == id).ToListAsync();
        if (listaBairros is null)
        {
            return response;            
        }
        foreach (var item in listaBairros)
        {
            response.Bairro.Add(item);
        }      
        return response;
    }

    public async Task<ImovelModelDTO> ConsultarMunicipioImovel(int id)
    {
        var response = new ImovelModelDTO(); 
        var listaMunicipios = await context.TabelaMunicipio.Where(p => p.IdProvincia == id).ToListAsync();
        if (listaMunicipios is null)
        {
            return response;            
        }
        foreach (var item in listaMunicipios)
        {
            response.Municipio.Add(item);
        }      
        return response;
    }

    public async Task<ImovelModelDTO> ConsultarPaisImovel()
    {
        var response = new ImovelModelDTO(); 
        var listaPaises = await context.TabelaPais!.ToListAsync();
        if (listaPaises is null)
        {
            return response;            
        }
        foreach (var item in listaPaises)
        {
            response.Pais.Add(item);
        }      
        return response;
    }

    public async Task<ImovelModelDTO> ConsultarProviciaImovel(int id)
    {
        var response = new ImovelModelDTO(); 
        var listaProvincias = await context.TabelaProvincia.Where(p => p.IdPais == id).ToListAsync();
        if (listaProvincias is null)
        {
            return response;            
        }
        foreach (var item in listaProvincias)
        {
            response.Provincia.Add(item);
        }      
        return response;
    }

    public async  Task<ImovelModelDTO> ConsultarRuaImovel(int id)
    {
        var response = new ImovelModelDTO(); 
        var listaRuas = await context.TabelaRua.Where(p => p.IdBairro == id).ToListAsync();
        if (listaRuas is null)
        {
            return response;            
        }
        foreach (var item in listaRuas)
        {
            response.Rua.Add(item);
        }      
        return response;
    }

    public async Task<string> EditarTipoImovel(TipoImovel tipo)
    {
        var tipoDb = await context.TabelaTipoImovel.FindAsync(tipo.Id);
        if (tipoDb is not null)
        {
            tipoDb.TipoImovelDesc = tipo.TipoImovelDesc;

            context.TabelaTipoImovel.Update(tipoDb);

            if (await context.SaveChangesAsync() == 1)
            {
                return "Tipo de imóvel editado com sucesso";
            }            
        }        
        return "não foi possível editar o tipo de imóvel";
    }

    public async Task<string> EliminarTipoImovel(int id, bool estado = false)
    {
        var tipo = await context.TabelaTipoImovel.FindAsync(id);
        if (tipo is not null)
        {
            tipo.Estado = estado;
            context.TabelaTipoImovel.Update(tipo);
            if (await context.SaveChangesAsync() == 1)
            {
                return "Tipo de imóvel escluído com sucesso";
            }            
        }
        return "Não foi possível excluir esse tipo de imóvel.";
    }

    public async Task<List<TipoImovel>?> ListarTipoImoveis()
    {
        return await context.TabelaTipoImovel.Where(i => i.Estado == true).OrderBy(i => i.TipoImovelDesc).ToListAsync();
    }

    public async Task<IEnumerable<NaturezaImovel>?> ListarNaturezaImovel()
    {
        return await context.TabelaNaturezaImovel.ToListAsync();
    }

    public async Task<string> UploadFotos(List<Foto> fotos, string codigo)
    {
        using(var transacao = await context.Database.BeginTransactionAsync())
        {
            try
            {
                foreach (var item in fotos)
                {
                    item.IdImovel = codigo;
                    await context.TabelaFoto.AddAsync(item);
                }
                if (await context.SaveChangesAsync() > 0)
                {
                    await transacao.CommitAsync();
                    return "Fotos enviadas com sucesso";
                }else
                {
                    await transacao.RollbackAsync();
                    return "Erro: lamentamos, infelizmente não conseguimos enviar as imagens para o servidor";
                }
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }
    }

    public async Task<List<ImovelModelResponse>> ListarImoveis(string estado)
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
        where imovel.Estado == estado
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
        
        foreach (var item in resultado)
        {
            item.ClienteProprietario.Senha = string.Empty;
            item.CorretorImovel.Senha = string.Empty;
        }
        return resultado;
    }


    public async Task<string> PublicarImovel(Publicacao publicacao)
    {
        string response = string.Empty;

        // Utiliza AsNoTracking() para evitar o rastreamento de 'p'
        var p = await context.TabelaPublicacao
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Codigo_Publicacao == publicacao.Codigo_Publicacao);

        if (p is not null && p.Estado == true)
        {
            context.Entry(publicacao).State = EntityState.Detached; // Evita o rastreamento duplicado de 'publicacao'
            response = "Este imóvel ainda está ativo";
        }
        else if (p is not null && p.Estado == false)
        {
            p.Estado = true;
            p.DataPublicacao = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Utc).ToUniversalTime();
            context.TabelaPublicacao.Update(p);
            response = "Imóvel reativado com sucesso";
        }
        else if (p is null)
        {
            publicacao.DataPublicacao = DateTime.SpecifyKind(DateTime.Now.Date, DateTimeKind.Utc).ToUniversalTime();
            
            publicacao.Estado = true;
            await context.TabelaPublicacao.AddAsync(publicacao);

            if (await context.SaveChangesAsync() > 0)
            {
                response = await MudarEstadoImovel(publicacao.Codigo_Publicacao);
                await gerente.NotificarClientes(publicacao.Codigo_Publicacao);
                return response;
            }
        }        

        return "Erro: Não foi possível publicar este imóvel, por favor verifique a sua conexão e tente novamente.";
    }

    private async Task<string> MudarEstadoImovel(string codigo)
    {
        var response = string.Empty;
        var imovel = await context.TabelaImovel.FirstOrDefaultAsync(i => i.Codigo == codigo);
        if (imovel is not null)
        {
            try
            {
                imovel.Estado = "Disponível";
                imovel.DataSolicitacao = DateTime.SpecifyKind(imovel.DataSolicitacao, DateTimeKind.Utc).ToUniversalTime();;
                context.TabelaImovel.Update(imovel);
                if (await context.SaveChangesAsync() > 0)
                {
                    response = "Imóvel publicado com sucesso";
                    return response;
                }      
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }      
        }
        response = "Erro";
        return response;
    }

    public async Task<string> EliminarImovel(string codigo)
    {
        var response = "O imóvel que pretende eliminar não existe";

        var p = await context.TabelaImovel.FirstOrDefaultAsync(p =>p.Codigo == codigo);
        if(p is not null)
        {
            context.TabelaImovel.Remove(p);
            if (await context.SaveChangesAsync() > 0)
            {
                response = "Imóvel eliminado com sucesso";
            }    
        }
        return response;
    }

    public async Task<List<ImovelModelResponse>?> PesquisarImovel(string codigo)
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
        where imovel.Codigo == codigo && imovel.Estado == "Disponível"
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
        return resultado;

    }

    public async Task<string> SolicitarImovel(SolicitacaoCliente solicitacao)
    {
        await context.TabelaSolicitacaoCliente.AddAsync(solicitacao);
        return await context.SaveChangesAsync() > 0 ? "Solicitação enviada com sucesso" : "Erro: Não foi possível enviar a sua solicitação. Por favor tente mais tarde";
    }
}
