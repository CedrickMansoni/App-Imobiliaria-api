using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.usuario;

public class Usuario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;
     [Column("senha")]
    public string Senha { get; set; } = string.Empty;
}
