using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.localizacao;

[Table("tabela06_localizacao")]
public class Localizacao
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_rua")]
    public int IdRua { get; set; }

    [Column("id_bairro")]
    public int IdBairro { get; set; }

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("id_provincia")]
    public int IdProvincia { get; set; }

    [Column("id_pais")]
    public int IdPais { get; set; }
}

