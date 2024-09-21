using System;

namespace App_Imobiliaria_api.Models.localizacao;

public class MunicipioModelRequest
{
    public Municipio? Municipio {get; set;}
    public int TotalBairro {get; set;}
    public List<BairroModelRequest>? Bairro {get; set;} = new();
}
