using System;
using App_Imobiliaria_api.Models.mensagem;

namespace App_Imobiliaria_api.Repository.Interfaces.mensagemInterface;

public interface ISmsRepository
{
    Task<MensagemResponse?> EnviarSMS(Mensagem mensagem);
}
