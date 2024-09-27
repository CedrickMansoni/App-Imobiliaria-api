using System;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IGerente 
{
    Task<string> CadastrarCorretor(Funcionario funcionario);
    Task<List<ModelResponse<Funcionario>>> ListarFuncionarios();
    Task<Pais?> CadastrarPais(Pais pais);
    Task<List<PaisModelRequest>> ListarPaises();
    Task<Provincia?> CadastrarProvincia(Provincia provincia);
    Task<Municipio?> CadastrarMunicipio(Municipio municipio);
    Task<Bairro?> CadastrarBairro(Bairro bairro);
    Task<string> RenovarToken(Token token); 
    Task<Token?> PegarToken();    
}
