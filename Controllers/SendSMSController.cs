using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Repository.Interfaces.mensagemInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            
            return Ok(await sms.EnviarSMS(mensagem));
            
        }
    }
}
