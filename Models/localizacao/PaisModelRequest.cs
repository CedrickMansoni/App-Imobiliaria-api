using System;

namespace App_Imobiliaria_api.Models.localizacao;

public class PaisModelRequest
{
    public Pais Pais {get; set;} = new();
    public int TotalProvincia {get; set;}
     public int TotalMunicipio {get; set;}
    public int TotalBairro {get; set;}
    public List<ProvinciaModelRequest>? Provincia {get; set;} = new();
}
