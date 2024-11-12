using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Imobiliaria_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IUsuario usuario;
        public PerfilController(IUsuario usuario)
        {
            this.usuario = usuario; 
        }
        private readonly string _storagePath = "/home/ckm/Imagens/StorageYula";

        [HttpPost]
        [Route("/editar/perfil")]
        public async Task<IActionResult> UploadFotos([FromForm] PerfilUsuario<Funcionario> perfil)
        {
            string fullPath = Path.Combine(_storagePath, perfil.Entidade!.Telefone); 

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if (perfil.Entidade == null && perfil.FotoPerfil == null)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }
           
            string caminhoArquivo = string.Empty;

            if (string.IsNullOrEmpty(perfil.FotoPerfil!.FileName))
            {
                caminhoArquivo = Path.Combine(fullPath, perfil.FotoPerfil!.FileName);
                var servidor = $"{UrlBase.UriBase.URI}";
                perfil.Entidade!.Avatar = $"{servidor}images/{perfil.Entidade.Telefone}/{perfil.FotoPerfil.FileName}";
            }

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await perfil.FotoPerfil.CopyToAsync(stream);
            }
            
            var response = await usuario.EditarPerfil(perfil);
            
            if (response)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("/get/perfil/{id}")]
        public async Task<IActionResult> GetPerfil(int id)=> Ok(await usuario.VerPerfil(id));

        [HttpPut]
        [Route("/editar/senha/funcionario")]
        public async Task<IActionResult> EditarSenha([FromBody] Funcionario funcionario)=> 
        Ok(await usuario.EditarSenhafuncionario(funcionario) == true ? "Senha editada com sucesso" : "Erro: Não foi possível editar a sua senha");
    }
}