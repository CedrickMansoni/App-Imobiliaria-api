using System;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.HomePage;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IGerente 
{
    Task<string> CadastrarCorretor(Funcionario funcionario);
    Task<int> EditarCorretor(Funcionario funcionario);
    Task<List<ModelResponse<Funcionario>>> ListarFuncionarios();
    Task<Pais?> CadastrarPais(Pais pais);
    Task<List<PaisModelRequest>> ListarPaises();
    Task<Provincia?> CadastrarProvincia(Provincia provincia);
    Task<Municipio?> CadastrarMunicipio(Municipio municipio);
    Task<Bairro?> CadastrarBairro(Bairro bairro);
    Task<Rua?> CadastrarRua(Rua rua);
    Task<string> RenovarToken(Token token); 
    Task<Token?> PegarToken(); 
    /* ------------------------------------------------------------ */  
    Task<HomePageModel> GetHomePage();
    /* ------------------------------------------------------------ */  
    Task<Funcionario> GetFuncionario(string telefone);
    Task<List<ModelResponse<Funcionario>>> ListarFuncionariosCategoria(string categoria);
    /* ------------------------------------------------------------ */  
    Task NotificarClientes(string codigo);
}
