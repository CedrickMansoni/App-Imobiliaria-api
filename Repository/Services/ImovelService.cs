using System;
using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Repository.Interfaces.imovelInterface;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.Repository.Services;

public class ImovelService : IImovel
{
    private readonly ImobContext.ImobContext context;
    public ImovelService(ImobContext.ImobContext context)
    {
        this.context = context;
    }

    public Task<string> CadastrarImovel(ImovelModelDTO imovel)
    {
        throw new NotImplementedException();
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
        var listaPaises = await context.TabelaPais.ToListAsync();
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
}
