using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;


[Table("tabela07_tipo_imovel")]
public class TipoImovel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("tipo_imovel")]
    public string TipoImovelDesc { get; set; } = string.Empty;

    [Column("estado")]
    public bool Estado { get; set; }
}
