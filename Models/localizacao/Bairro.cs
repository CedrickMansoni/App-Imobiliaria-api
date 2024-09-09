using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela04_bairro")]
public class Bairro
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_bairro")]
    public string NomeBairro { get; set; } = string.Empty;

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }
}

