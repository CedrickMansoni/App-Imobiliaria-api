using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.mensagem;

[Table("tabela17_notificar_cliente")]
public class NotificarCliente
{
    [Column("id")]
    public int Id { get; set; }

    [Column("mensagem")]
    public string Mensagem { get; set; } = string.Empty;

    [Column("id_publicacao")]
    public int IdPublicacao { get; set; }

    [Column("id_solicitacao")]
    public int IdSolicitacao { get; set; }

    [Column("data_notificacao")]
    public DateTime DataNotificacao { get; set; }
}
