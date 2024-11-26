using System;

namespace App_Imobiliaria_api.Models.imovel;

public class FavoritoDTO
{
    public int Id {get; set;}
    
    public List<string> CodigoImovel {get; set;} = new List<string>();
    
    public int ClienteId { get; set; }
}
