using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Imobiliaria_api.Models.DropBox;
[Table("tabela20_token")]
public class Token
{
    [Column("id")]
    public int Id { get; set; }

    [Column("token")]
    public string TokenAccess { get; set; } = string.Empty;
}
