using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela14_publicacao")]
public class Publicacao
{
    [Key]
    [Column("codigo_publicacao")]
    public string Codigo_Publicacao { get; set; } = string.Empty;
    
    [Column("data_publicacao")]
    public DateTime DataPublicacao { get; set; }

    [Column("gostei")]
    public int Gostei { get; set; }

    [Column("nao_gostei")]
    public int NaoGostei { get; set; }

    [Column("total_comentarios")]
    public int TotalComentarios { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("data_conclusao")]
    public DateTime DataConclusao { get; set; }
}

