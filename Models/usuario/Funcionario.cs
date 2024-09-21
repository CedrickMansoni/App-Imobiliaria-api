using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.usuario;

[Table("tabela09_funcionario")]
public class Funcionario : Usuario
{  
    [Column("estado")]
    public string Estado { get; set; } = string.Empty;

    [Column("id_provincia")]
    public int IdProvincia { get; set; }

    [Column("nivel")]
    public string Nivel { get; set; } = string.Empty;
}
