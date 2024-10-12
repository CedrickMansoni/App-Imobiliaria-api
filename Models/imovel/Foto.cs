using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela13_foto")]
public class Foto
{
    [Column("id")]
    public int Id { get; set; }

    [Column("imagem")]
    public string Imagem { get; set; } = string.Empty;

    [Column("codigo_imovel")]
    public string IdImovel { get; set; } = string.Empty;
}
