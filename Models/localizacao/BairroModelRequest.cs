using System;

namespace App_Imobiliaria_api.Models.localizacao;

public class BairroModelRequest
{
    public Bairro? Bairro {get; set;}
    public List<Rua>? Rua {get; set;} = new();
}
