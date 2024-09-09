using System;
using System.ComponentModel.DataAnnotations.Schema;
using App_Imobiliaria_api.Enumerables;

namespace App_Imobiliaria_api.Models.usuario;

[Table("tabela09_funcionario")]
public class Funcionario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("estado")]
    public EstadoFuncionario Estado { get; set; }

    [Column("senha")]
    public string Senha { get; set; } = string.Empty;

    [Column("id_provincia")]
    public int IdProvincia { get; set; }

    [Column("nivel")]
    public int Nivel { get; set; }
}
