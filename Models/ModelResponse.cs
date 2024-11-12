using System;

namespace App_Imobiliaria_api.Models;

public class ModelResponse<T>
{
    public T? Dados{get; set;}
    public string Mensagem {get; set;} = string.Empty;
    public string UserType {get;set;} = string.Empty;
    public string Estado {get; set;} = string.Empty;
    public string Avatar {get; set;} = string.Empty;
}
