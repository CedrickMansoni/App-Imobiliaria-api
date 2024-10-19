using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace App_Imobiliaria_api.Models.imovel;

public class FotoDTO
{
    public List<IFormFile> Ficheiros {get; set;} = new List<IFormFile>();
}
