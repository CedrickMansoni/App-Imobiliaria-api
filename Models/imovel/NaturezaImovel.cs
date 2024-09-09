using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela08_natureza_imovel")]
public class NaturezaImovel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_tipo_imovel")]
    public int IdTipoImovel { get; set; }

    [Column("dimensao")]
    public string Dimensao { get; set; } = string.Empty;

    [Column("tipologia")]
    public string Tipologia { get; set; } = string.Empty;

    [Column("id_localizacao")]
    public int IdLocalizacao { get; set; }
}

