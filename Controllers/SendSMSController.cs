using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Repository.Interfaces.mensagemInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App_Imobiliaria_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSMSController : ControllerBase
    {
        private readonly ISmsRepository sms;
        public SendSMSController(ISmsRepository sms)
        {
            this.sms = sms;
        }
        [HttpPost]
        [Route("/enviar/sms")]
        public async Task<IActionResult> SendSMS(Mensagem mensagem)
        {

            if (mensagem is null) return BadRequest("Mensagem vazia");

            mensagem.ChaveEntidade = SMS.Agente.Key;
            mensagem.Accao = "enviar_sms";

            var message = new EnviarMensagem()
            {
                Mensagem = mensagem,
            };
            
            return Ok(await sms.EnviarSMS(message));
            
        }
    }
}
