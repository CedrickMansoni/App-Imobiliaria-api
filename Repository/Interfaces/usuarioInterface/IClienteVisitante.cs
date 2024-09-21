using System;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IClienteVisitante : IUsuarioPublico
{
    public Task<string> CriarConta(Usuario usuario, string tipoConta);
}
