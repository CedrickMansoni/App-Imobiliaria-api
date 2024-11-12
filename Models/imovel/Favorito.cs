using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;
[Table("tabela22_favorito")]
public class Favorito
{
    [Column("id")]
    public int Id {get; set;}

    [Column("data_favorito")]
    public DateTime Data {get; set;}
    
    [Column("codigo_publicacao")]
    public string CodigoImovel {get; set;} = string.Empty;
    [Column("id_cliente_solicitante")]
    public int ClienteId { get; set; }
}
