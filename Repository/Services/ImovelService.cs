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
    public async Task<TipoImovel?> CadastrarTipoImovel(TipoImovel tipo)
    {
        var tipoDb = await context.TabelaTipoImovel.FirstOrDefaultAsync(t => t.TipoImovelDesc == tipo.TipoImovelDesc);
        if (tipoDb is null)
        {
            await context.TabelaTipoImovel.AddAsync(tipo);
            if (await context.SaveChangesAsync() == 1)
            {
                return await context.TabelaTipoImovel.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            }
        }
        return null;
    }

    public async Task<string> EditarTipoImovel(TipoImovel tipo)
    {
        var tipoDb = await context.TabelaTipoImovel.FirstOrDefaultAsync(t => t.Id == tipo.Id);
        if (tipoDb is not null)
        {
            tipoDb.TipoImovelDesc = tipo.TipoImovelDesc;
            tipoDb.Estado = tipo.Estado;
            context.TabelaTipoImovel.Update(tipoDb);
            if (await context.SaveChangesAsync() == 1)
            {
                return "Tipo de imóvel editado com sucesso";
            }            
        }        
        return "não foi possível editar o tipo de imóvel";
    }

    public Task<string> EliminarTipoImovel(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TipoImovel>?> ListarTipoImoveis()
    {
        return await context.TabelaTipoImovel.OrderBy(i => i.TipoImovelDesc).ToListAsync();
    }
}
