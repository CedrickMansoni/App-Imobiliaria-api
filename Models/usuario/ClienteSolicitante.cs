using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.usuario;

[Table("tabela11_cliente_solicitante")]
public class ClienteSolicitante : Usuario
{
    [Column("estado")]
    public EstadoCliente Estado { get; set; }
}
