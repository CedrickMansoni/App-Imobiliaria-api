using System;

namespace App_Imobiliaria_api.Models.localizacao;

public class ProvinciaModelRequest
{
    public Provincia? Provincia {get; set;}
    public int TotalMunicipio {get; set;}
    public List<MunicipioModelRequest>? Municipio {get; set;} = new();
}
