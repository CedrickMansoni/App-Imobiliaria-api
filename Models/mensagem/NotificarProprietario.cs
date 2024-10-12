using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.mensagem;


[Table("tabela15_notificar_proprietario")]
public class NotificarProprietario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_corretor")]
    public int IdCorretor { get; set; }

    [Column("id_cliente_proprietario")]
    public int IdClienteProprietario { get; set; }

    [Column("codigo_imovel")]
    public string IdImovel { get; set; } = string.Empty;

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("data_notificacao")]
    public DateTime DataNotificacao { get; set; }
}

