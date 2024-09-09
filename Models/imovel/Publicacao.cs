using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela14_publicacao")]
public class Publicacao
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_corretor")]
    public int IdCorretor { get; set; }

    [Column("id_imovel")]
    public int IdImovel { get; set; }

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

