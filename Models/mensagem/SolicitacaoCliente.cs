using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.mensagem;

[Table("tabela16_solicitacao_cliente")]
public class SolicitacaoCliente
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_cliente_solicitante")]
    public int IdClienteSolicitante { get; set; }

    [Column("preco_minimo")]
    public decimal PrecoMinimo { get; set; }

    [Column("preco_maximo")]
    public decimal PrecoMaximo { get; set; }

    [Column("id_tipo_imovel")]
    public int IdTipoImovel { get; set; }

    [Column("id_localizacao")]
    public int IdLocalizacao { get; set; }
}

