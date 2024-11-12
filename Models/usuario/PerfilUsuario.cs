using System;

namespace App_Imobiliaria_api.Models.usuario;

public class PerfilUsuario<T>
{
    public T? Entidade {get; set;}
    public IFormFile? FotoPerfil {get; set;}
}
