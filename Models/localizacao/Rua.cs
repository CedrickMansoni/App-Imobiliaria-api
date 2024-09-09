using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela05_rua")]
public class Rua
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_rua")]
    public string NomeRua { get; set; } = string.Empty;

    [Column("id_bairro")]
    public int IdBairro { get; set; }
}

