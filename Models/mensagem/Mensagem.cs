using System;
using System.Text.Json.Serialization;

namespace App_Imobiliaria_api.Models.mensagem;

public class Mensagem
{
    [JsonPropertyName("accao")]
    public string Accao { get; set; } = "enviar_sms";

    [JsonPropertyName("chave_entidade")]
    public string ChaveEntidade { get; set; } = SMS.Agente.Key;

    [JsonPropertyName("destinatario")]
    public string Destinatario { get; set; } = string.Empty;

    [JsonPropertyName("descricao_sms")]
    public string DescricaoSms { get; set; } = string.Empty;
}
