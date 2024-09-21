using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.usuario;

[Table("tabela10_cliente_proprietario")]
public class ClienteProprietario : Usuario
{

    [Column("estado")]
    public EstadoClienteProprietario Estado { get; set; }
}

