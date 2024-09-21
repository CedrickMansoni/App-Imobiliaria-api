using System;
using App_Imobiliaria_api.Models.imovel;

namespace App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;

public interface IUsuarioPublico
{
    public Task<List<Publicacao>> VerPublicacoes();
    public Task<List<Publicacao>> PesquisarPublicacao(string valor);
}
