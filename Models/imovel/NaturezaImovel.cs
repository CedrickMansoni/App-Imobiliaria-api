using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela08_caracteristica_imovel")]
public class NaturezaImovel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("caracteristica")]
    public string Caracteristica { get; set; } = string.Empty;

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("id_tipo_imovel")]
    public int IdTipoImovel { get; set; } 
}

