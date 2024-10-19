using System;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Models.imovel;

public class ImovelModelResponse
{
    public Imovel Imovel {get; set;} = new();
    public Pais Pais {get; set;} = new();
    public Provincia Provincia {get; set;} = new();
    public Municipio Municipio {get; set;} = new();
    public Bairro Bairro {get; set;} = new(); 
    public Rua Rua {get; set;} = new();
    public TipoImovel? TipoImovel {get; set;} = new();
    public TipoPublicacao TipoPublicacao{get; set;} = new();
    public NaturezaImovel? NaturezaImovel {get; set;} = new();
    public List<Foto>? Fotos {get; set;} = new();
    public string? FotoPrincipal {get; set;} = string.Empty;

    /* DADOS DO PROPRIETÁRIO DO IMÓVEL ------------------------------------------------ */
    public ClienteProprietario ClienteProprietario {get; set;} = new();

    /* DADOS DO CORRETOR RESPONSÁVEL---------------------------------------------------- */
    public Funcionario CorretorImovel {get; set;} = new();
}
