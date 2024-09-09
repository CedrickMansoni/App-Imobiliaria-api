using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela02_provincia")]
public class Provincia
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_provincia")]
    public string NomeProvincia { get; set; } = string.Empty;

    [Column("id_pais")]
    public int IdPais { get; set; }
}

