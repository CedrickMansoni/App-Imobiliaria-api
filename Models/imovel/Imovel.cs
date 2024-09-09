using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.imovel;

[Table("tabela12_imovel")]
public class Imovel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_cliente_proprietario")]
    public int IdClienteProprietario { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("data_solicitacao")]
    public DateTime DataSolicitacao { get; set; }

    [Column("estado")]
    public EstadoImovel Estado { get; set; }

    [Column("tipo_publicidade")]
    public TipoPublicidadeImovel TipoPublicidade { get; set; }

    [Column("preco")]
    public decimal Preco { get; set; }

    [Column("id_natureza_imovel")]
    public int IdNaturezaImovel { get; set; }
}