using System;
using System.Text.Json.Serialization;

namespace App_Imobiliaria_api.Models.mensagem;

public class EnviarMensagem
{
    [JsonPropertyName("mensagem")]
    public required Mensagem Mensagem { get; set; }
}
