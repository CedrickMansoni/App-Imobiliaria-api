using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;
[Table("tabela21_venda_arrendamento")]
public class TipoPublicacao
{
    [Column("id")]
    public int Id {get; set;}
    
    [Column("descricao")]
    public string Descricao {get; set;} = string.Empty;
}
