using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Repository.Interfaces.imovelInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace App_Imobiliaria_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {
        private readonly IImovel imovel;
        public ImovelController(IImovel imovel)
        {
            this.imovel = imovel;
        }

        [HttpPost]
        [Route("/cadastrar/tipo/imovel")]
        public async Task<IActionResult> CadastrarTipoImovel([FromBody] TipoImovel tipo)
        {
            var response = await imovel.CadastrarTipoImovel(tipo);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest("Erro: Não foi possível cadastrar o tipo de imóvel");            
        }

        [HttpGet]
        [Route("/listar/tipo/imovel")]
        public async Task<IActionResult> ListarTipoImovel()
        {
            return Ok(await imovel.ListarTipoImoveis());
        }

        [HttpPut]
        [Route("/editar/tipo/imovel")]
        public async Task<IActionResult> EditarTipoImovel([FromBody] TipoImovel tipo)
        {
            var response = await imovel.EditarTipoImovel(tipo);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("/eliminar/tipo/imovel/{id}")]
        public async Task<IActionResult> EliminarTipoImovel(int id)
        {
            var response = await imovel.EliminarTipoImovel(id);
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("/listar/paises")]
        public async Task<IActionResult> ListarPaisesImovel()
        {
            return Ok(await imovel.ConsultarPaisImovel());
        }

        [HttpGet]
        [Route("/listar/provincias/{id}")]
        public async Task<IActionResult> ListarProvinciasImovel(int id)
        {
            return Ok(await imovel.ConsultarProviciaImovel(id));
        }

        [HttpGet]
        [Route("/listar/municipios/{id}")]
        public async Task<IActionResult> ListarMunicipiosImovel(int id)
        {
            return Ok(await imovel.ConsultarMunicipioImovel(id));
        }

        [HttpGet]
        [Route("/listar/bairros/{id}")]
        public async Task<IActionResult> ListarBairrosImovel(int id)
        {
            return Ok(await imovel.ConsultarBairroImovel(id));
        }

        [HttpGet]
        [Route("/listar/ruas/{id}")]
        public async Task<IActionResult> ListarRuasImovel(int id)
        {
            return Ok(await imovel.ConsultarRuaImovel(id));
        }

        [HttpPost]
        [Route("/cadastrar/imovel")]
        public async Task<IActionResult> CadastrarImovel([FromBody] ImovelModelDTO imovelRequest)
        {
            var response = await imovel.CadastrarImovel(imovelRequest);
            if (response.Contains("Erro"))
            {
                return BadRequest(response);                  
            }
            return Ok(response);        
        }
        
        private readonly string _storagePath = "/home/ckm/Imagens/StorageYula";

        [HttpPost]
        [Route("/upload/fotos/{codigo}")]
        public async Task<IActionResult> UploadFotos([FromForm] List<IFormFile> files, string codigo)
        {
            string fullPath = Path.Combine(_storagePath, codigo); 

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if (files == null || files.Count == 0)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }

            List<string> caminhosSalvos = [];

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string caminhoArquivo = Path.Combine(fullPath, file.FileName);

                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    caminhosSalvos.Add(file.FileName);
                }
            }

            var servidor = $"{UrlBase.UriBase.URI}";
            var fotos = new List<Foto>();

            foreach (var item in caminhosSalvos)
            {
                fotos.Add(new Foto { Imagem = $"{servidor}images/{item}", IdImovel = codigo });
            }

            var response = await imovel.UploadFotos(fotos, codigo);
            
            if (response.Contains("sucesso"))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet]
        [Route("/listar/imoveis/{estado}")]
        public async Task<IActionResult> ListarRuasImovel(string estado)
        {
            return Ok(await imovel.ListarImoveis(estado));
        }

        [HttpPost]
        [Route("/publicar/imovel")]
        public async Task<IActionResult> PublicarImovel([FromBody] Publicacao publicacao)
        {
            var response = await imovel.PublicarImovel(publicacao);
            if (response.Contains("sucesso") || response.Contains("activo"))
            {
                return Ok(response);
            }
            return BadRequest(response);            
        }

        [HttpPut]
        [Route("/eliminar/imovel/{codigo}")]
        public async Task<IActionResult> EditarTipoImovel(string codigo)
        {
            var response = await imovel.EliminarImovel(codigo);
            if (response.Contains("sucesso"))
            {
                string caminhoPasta = $"/home/ckm/Imagens/StorageYula/{codigo}";
        
                try
                {
                    if (Directory.Exists(caminhoPasta))
                    {
                        Directory.Delete(caminhoPasta, true); 
                        Console.WriteLine($"Pasta {caminhoPasta} eliminada com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"A pasta {caminhoPasta} não foi encontrada.");
                    }
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao eliminar a pasta: {ex.Message}");
                    return BadRequest("Erro ao eliminar o diretório associado ao imóvel.");
                }
            }
            return BadRequest(response);
        }
    }
}
