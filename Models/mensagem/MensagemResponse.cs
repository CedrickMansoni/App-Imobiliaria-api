using System;
using System.Text.Json.Serialization;

namespace App_Imobiliaria_api.Models.mensagem;

public class MensagemResponse
{
        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;

        [JsonPropertyName("custo")]
        public int Custo { get; set; }
}
