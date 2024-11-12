using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela12_imovel")]
public class Imovel
{
    [Key]
    [Column("codigo_imovel")]
    public string Codigo { get; set; } = string.Empty;

    [Column("id_cliente_proprietario")]
    public int IdClienteProprietario { get; set; }

    [Column("id_corretor")]
    public int IdCorretor { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("data_solicitacao")]
    public DateTime DataSolicitacao { get; set; }

    [Column("estado")]
    public string Estado { get; set; } = string.Empty;

    [Column("tipo_publicidade")]
    public int TipoPublicidade { get; set; } 

    [Column("preco")]
    public decimal Preco { get; set; }    

    [Column("id_natureza_imovel")]
    public int IdNaturezaImovel { get; set; }

    [Column("id_localizacao")]
    public int IdLocalizacao { get; set; }
}