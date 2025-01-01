using System;
using System.Text;
using System.Text.Json;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Repository.Interfaces.mensagemInterface;

namespace App_Imobiliaria_api.Repository.Services;

public class SmsService : ISmsRepository
{
    HttpClient client;
    JsonSerializerOptions option;
    public SmsService( )
    {
        client = new HttpClient();
        option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    }
    public async Task<MensagemResponse?> EnviarSMS(Mensagem mensagem)
    {
        string json = JsonSerializer.Serialize<Mensagem>(mensagem, option);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(UrlBase.UriBase.Sms, content);

        MensagemResponse smsResponse = new();
        if (response.IsSuccessStatusCode)
        {
            using(var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<MensagemResponse>(responseStream, option);              
            }            
        }
        return null;
    }
}
