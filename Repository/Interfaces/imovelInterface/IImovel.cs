using System;
using App_Imobiliaria_api.Models.imovel;

namespace App_Imobiliaria_api.Repository.Interfaces.imovelInterface;

public interface IImovel
{
    Task<TipoImovel?>CadastrarTipoImovel(TipoImovel tipo);
    Task<List<TipoImovel>?> ListarTipoImoveis();
    Task<string> EditarTipoImovel(TipoImovel tipo);
    Task<string> EliminarTipoImovel(int id);  
}
