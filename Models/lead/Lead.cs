using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.lead;

[Table("tabela18_lead")]
public class Lead
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_publicacao")]
    public int IdPublicacao { get; set; }

    [Column("id_cliente_solicitante")]
    public int IdClienteSolicitante { get; set; }

    [Column("estado")]
    public EstadoLead Estado { get; set; }

    [Column("data_abertura")]
    public DateTime DataAbertura { get; set; }

    [Column("data_conclusao")]
    public DateTime DataConclusao { get; set; }
}

