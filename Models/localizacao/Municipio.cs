using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela03_municipio")]
public class Municipio
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_municipio")]
    public string NomeMunicipio { get; set; } = string.Empty;

    [Column("id_provincia")]
    public int IdProvincia { get; set; }
}
