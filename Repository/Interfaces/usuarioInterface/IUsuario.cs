using System;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IUsuario
{    
    Task<ModelResponse<Usuario>?> FazerLogin(ModelResponse<Usuario> usuario);
    Task<ModelResponse<Funcionario>> VerPerfil(int id);
    Task<bool> EditarPerfil(string avatar, int id);
    Task<bool> EditarSenhafuncionario(Funcionario funcionario);
}
