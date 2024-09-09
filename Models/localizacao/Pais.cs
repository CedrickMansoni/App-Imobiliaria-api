using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela01_pais")]
public class Pais
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_pais")]
    public string NomePais { get; set; } = string.Empty;
}
