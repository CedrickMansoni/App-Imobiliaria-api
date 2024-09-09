using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.mensagem;

[Table("tabela19_chat")]
public class Chat
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_corretor")]
    public int IdCorretor { get; set; }

    [Column("id_lead")]
    public int IdLead { get; set; }

    [Column("id_cliente_solicitante")]
    public int IdClienteSolicitante { get; set; }

    [Column("mensagem")]
    public string Mensagem { get; set; } = string.Empty;

    [Column("data_mensagem")]
    public DateTime DataMensagem { get; set; }

    [Column("pergunta_mensagem")]
    public string PerguntaMensagem { get; set; } = string.Empty;

    [Column("id_pergunta")]
    public int IdPergunta { get; set; }

    [Column("data_conclusao")]
    public DateTime DataConclusao { get; set; }
}

