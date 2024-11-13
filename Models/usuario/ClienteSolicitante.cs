using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.usuario;

[Table("tabela11_cliente_solicitante")]
public class ClienteSolicitante 
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Column("senha")]
    public string Senha { get; set; } = string.Empty;
    
    [Column("estado")]
    public EstadoCliente Estado { get; set; }
}
