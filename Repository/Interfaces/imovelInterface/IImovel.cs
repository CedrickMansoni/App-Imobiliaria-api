using System;
using App_Imobiliaria_api.Enumerables;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.mensagem;

namespace App_Imobiliaria_api.Repository.Interfaces.imovelInterface;

public interface IImovel
{
    Task<string>CadastrarTipoImovel(TipoImovel tipo);
    Task<List<TipoImovel>?> ListarTipoImoveis();
    Task<IEnumerable<NaturezaImovel>?> ListarNaturezaImovel();
    Task<string> EditarTipoImovel(TipoImovel tipo);
    Task<string> EliminarTipoImovel(int id, bool tipo = false); 
    /*-----------------------------------------------------------------*/
    Task<NaturezaImovel> CadastrarNaturezaImove(NaturezaImovel natureza); 
    Task<string> CadastrarImovel(ImovelModelDTO imovel); 
    Task<ImovelModelDTO> ConsultarPaisImovel(); 
    Task<ImovelModelDTO> ConsultarProviciaImovel(int id); 
    Task<ImovelModelDTO> ConsultarMunicipioImovel(int id); 
    Task<ImovelModelDTO> ConsultarBairroImovel(int id); 
    Task<ImovelModelDTO> ConsultarRuaImovel(int id); 
    /*-----------------------------------------------------------------*/
    Task<string> UploadFotos(List<Foto> fotos, string codigo);
    /*-----------------------------------------------------------------*/
    Task<List<ImovelModelResponse>> ListarImoveis(string estado);
     Task<List<ImovelModelResponse>?> PesquisarImovel(string codigo);
    /*-----------------------------------------------------------------*/
    Task<string> PublicarImovel(Publicacao publicacao);
    Task<string> EliminarImovel(string codigo);

    Task<string> SolicitarImovel(SolicitacaoCliente solicitacao);
}
