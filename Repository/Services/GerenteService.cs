using System;
using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace App_Imobiliaria_api.Repository.Services;

public class GerenteService : IGerente
{
    private readonly ImobContext.ImobContext context;
    public GerenteService(ImobContext.ImobContext context)
    {
        this.context = context;
    }

    public async Task<Bairro?> CadastrarBairro(Bairro bairro)
    {
        await context.TabelaBairro.AddAsync(bairro);
        if (await context.SaveChangesAsync() == 1)
        {
            return await context.TabelaBairro.OrderBy(p => p.Id).LastOrDefaultAsync(); 
        }
        return null;
    }
   
    public async Task<Municipio?> CadastrarMunicipio(Municipio municipio)
    {
        await context.TabelaMunicipio.AddAsync(municipio);
        if (await context.SaveChangesAsync() == 1)
        {
            return await context.TabelaMunicipio.OrderBy(p => p.Id).LastOrDefaultAsync(); 
        }
        return null;
    }

    public async Task<Pais?> CadastrarPais(Pais pais)
    {
        await context.TabelaPais.AddAsync(pais);
        if (await context.SaveChangesAsync() == 1)
        {
            return await context.TabelaPais.OrderBy(p => p.Id).LastOrDefaultAsync(); 
        }
        return null;
    }

    public async Task<Provincia?> CadastrarProvincia(Provincia provincia)
    {
        await context.TabelaProvincia.AddAsync(provincia);
        if (await context.SaveChangesAsync() == 1)
        {
            return await context.TabelaProvincia.OrderBy(p => p.Id).LastOrDefaultAsync(); 
        }
        return null;
    }

    public async Task<List<PaisModelRequest>> ListarPaises()
    {
        var paisesDB = await context.TabelaPais.ToListAsync();
        var provinciasDB = await context.TabelaProvincia.ToListAsync();
        var municipiosDB = await context.TabelaMunicipio.ToListAsync();
        var bairrosDB = await context.TabelaBairro.ToListAsync();
        /* ------------------------------------------------------------*/
        var modeloPais = new List<PaisModelRequest>();
        var modeloProvincia = new List<ProvinciaModelRequest>();
        var modelomunicipio = new List<MunicipioModelRequest>();
        var modeloBairro = new List<BairroModelRequest>();
        /* ------------------------------------------------------------*/
        foreach (var pais in paisesDB)
        {
            var p = new PaisModelRequest(){ Pais = pais};
            modeloPais.Add(p);
        } 
        foreach (var provincia in provinciasDB)
        {
            var p = new ProvinciaModelRequest(){ Provincia = provincia};
            modeloProvincia.Add(p);
        }  
        foreach (var municipio in municipiosDB)
        {
            var p = new MunicipioModelRequest(){ Municipio = municipio};
            modelomunicipio.Add(p);
        } 
        foreach (var bairro in bairrosDB)
        {
            var p = new BairroModelRequest(){ Bairro = bairro};
            modeloBairro.Add(p);
        }        
        /* ------------------------------------------------------------*/
        var paislista = new List<PaisModelRequest>();

        foreach (var pais in modeloPais)
        {            
            foreach (var provincia in modeloProvincia)
            {
                if(provincia.Provincia is not null && pais.Pais is not null && pais.Provincia is not null)
                if (provincia.Provincia.IdPais == pais.Pais.Id)
                {
                    foreach (var municipio in modelomunicipio)
                    {
                        if(municipio.Municipio is not null)
                        if (municipio.Municipio.IdProvincia == provincia.Provincia.Id)
                        {
                            foreach (var bairro in modeloBairro)
                            {
                                if(bairro.Bairro is not null && municipio.Bairro is not null)
                                if (bairro.Bairro.IdMunicipio == municipio.Municipio.Id)
                                {
                                    municipio.Bairro.Add(bairro);
                                    municipio.TotalBairro++;
                                    pais.TotalBairro = municipio.TotalBairro;
                                }
                            }
                            if(provincia.Municipio is not null)
                            provincia.Municipio.Add(municipio);
                            provincia.TotalMunicipio++;
                            pais.TotalMunicipio = provincia.TotalMunicipio;
                        }
                    }
                    pais.Provincia.Add(provincia); 
                    pais.TotalProvincia++;                    
                }
            }
            paislista.Add(pais);
        }
        return paislista.OrderBy(p => p.Pais.NomePais).ToList();
    }

    public async Task<string> CadastrarCorretor(Funcionario funcionario)
    {
        await context.TabelaFuncionarios.AddAsync(funcionario);
        if (await context.SaveChangesAsync() == 1)
        {
            return "Funcionario cadastrado com sucesso";
        }
        return "Erro: Não foi possível cadastrar o funcionário. Por favor tente novamente";
    }

    public async Task<int> EditarCorretor(Funcionario funcionario)
    {
        var f = await context.TabelaFuncionarios.FirstOrDefaultAsync(f => f.Id == funcionario.Id);
        if (f is not null)
        {
            f.Nome = funcionario.Nome;
            f.Telefone = funcionario.Telefone;
            f.Email = funcionario.Email;
            f.Estado = funcionario.Estado;
            f.IdProvincia = funcionario.IdProvincia;
            f.Nivel = funcionario.Nivel;

            // Marca apenas os campos desejados como modificados
            context.Entry(f).Property(x => x.Nome).IsModified = true;
            context.Entry(f).Property(x => x.Telefone).IsModified = true;
            context.Entry(f).Property(x => x.Email).IsModified = true;
            context.Entry(f).Property(x => x.Estado).IsModified = true;
            context.Entry(f).Property(x => x.IdProvincia).IsModified = true;
            context.Entry(f).Property(x => x.Nivel).IsModified = true;

            return await context.SaveChangesAsync();
        }        
        return 0;    
    }


    public async Task<List<ModelResponse<Funcionario>>> ListarFuncionarios()
    {
        var modeleResponse = new List<ModelResponse<Funcionario>>();
        
        var lista = await context.TabelaFuncionarios.ToListAsync();

        foreach (var item in lista)
        {
            var model = new ModelResponse<Funcionario>();
            item.Senha = string.Empty;
            model.Dados = item;
            if (!string.IsNullOrEmpty(item.Avatar))
            {
                model.Avatar = item.Avatar;
            }            
            var provincia = await context.TabelaProvincia.FindAsync(item.IdProvincia);
            model.UserType = item.Nivel;
            model.Estado = item.Estado;
            if(provincia is not null)
            {
                model.Mensagem = provincia.NomeProvincia; 
            }                       
            modeleResponse.Add(model);                        
        }
        
        return modeleResponse;
    }

    public async Task<List<ModelResponse<Funcionario>>> ListarFuncionariosCategoria(string categoria)
    {
        var modeleResponse = new List<ModelResponse<Funcionario>>();
        
        var lista = await context.TabelaFuncionarios.Where(f => f.Nivel == categoria).ToListAsync();

        foreach (var item in lista)
        {
            var model = new ModelResponse<Funcionario>();
            item.Senha = string.Empty;
            model.Dados = item;
            if (!string.IsNullOrEmpty(item.Avatar))
            {
                model.Avatar = item.Avatar;
            }            
            var provincia = await context.TabelaProvincia.FindAsync(item.IdProvincia);
            model.UserType = item.Nivel;
            model.Estado = item.Estado;
            if(provincia is not null)
            {
                model.Mensagem = provincia.NomeProvincia; 
            }                       
            modeleResponse.Add(model);                        
        }
        
        return modeleResponse;
    }

    public async Task<string> RenovarToken(Token token)
    {
        var DbToken = await context.TabelaToken.FindAsync(1);
        if (DbToken is null)
        {
            await context.TabelaToken.AddAsync(token);            
        }else
        {
            DbToken.TokenAccess = token.TokenAccess;
            context.TabelaToken.Update(DbToken);
        }
        if (await context.SaveChangesAsync() == 1)
        {
            return "Token configurado com sucesso";
        }
        return "Erro:\nNão foi possível configurar o token, por favor tente novamente";
    }

    public async Task<Token?> PegarToken()
    {
        return await context.TabelaToken.FindAsync(1);
    }

    public async Task<Rua?> CadastrarRua(Rua rua)
    {
        var ruaDb = await context.TabelaRua.FirstOrDefaultAsync(r => r.NomeRua == rua.NomeRua && r.IdBairro == rua.IdBairro);
        if (ruaDb is null)
        {
            await context.TabelaRua.AddAsync(rua);
            if (await context.SaveChangesAsync() == 1)
            {
                return await context.TabelaRua.FirstOrDefaultAsync(r => r.NomeRua == rua.NomeRua && r.IdBairro == rua.IdBairro);
            }
        }
        return rua;
    }

    public Task<Funcionario> GetFuncionario(string telefone)
    {
        throw new NotImplementedException();
    }
}
