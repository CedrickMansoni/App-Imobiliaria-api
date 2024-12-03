using System;
using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Models;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.HomePage;
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
        await context.TabelaPais!.AddAsync(pais);
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
        var paisesDB = await context.TabelaPais!.ToListAsync();
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
        funcionario.Avatar = "http://192.168.1.158:5254/images/fotoperfil/avatar.jpg";
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

    public async Task<HomePageModel> GetHomePage()
    {
        var homePage = new HomePageModel();
        
        var funcionarioTabela = await context.TabelaFuncionarios.ToListAsync();
        homePage.FuncionarioTotal = funcionarioTabela.Count();
        homePage.FuncionarioActivos = funcionarioTabela.Where(f => f.Estado == "Activo").Count();
        homePage.FuncionarioInactivos = funcionarioTabela.Where(f => f.Estado == "Inactivo").Count();
        homePage.FuncionarioGerentes = funcionarioTabela.Where(f => f.Nivel == "Gerente").Count();
        homePage.FuncionarioCorretores = funcionarioTabela.Where(f => f.Nivel == "Corretor").Count();

        /*---------------------------------------------------------------------------------------------*/

        var clienteProprietarioTabela = await context.TabelaClientesProprietarios.ToListAsync();
        homePage.ClienteProprietarios = clienteProprietarioTabela.Count();

        /*---------------------------------------------------------------------------------------------*/

        var clienteConsumidor = await context.TabelaClientesSolicitantes.ToListAsync();
        homePage.ClienteConsumidores = clienteConsumidor.Count();

        /*---------------------------------------------------------------------------------------------*/

        var imoveisTabela = await context.TabelaImovel.ToListAsync();
        homePage.ImoveisCadastrados = imoveisTabela.Count();
        homePage.ImoveisPendentes = imoveisTabela.Where(i => i.Estado == "Pendente").Count();
        homePage.ImoveisDisponiveis = imoveisTabela.Where(i => i.Estado == "Disponível").Count();
        homePage.ImoveisPublicados = imoveisTabela.Where(i => i.Estado != "Pendente").Count();

        /*---------------------------------------------------------------------------------------------*/

        homePage.ImoveisParaVenda = imoveisTabela.Where(i => i.TipoPublicidade == 2).Count();
        homePage.ImoveisParaArrendamento = imoveisTabela.Where(i => i.TipoPublicidade == 1).Count();

        /*---------------------------------------------------------------------------------------------*/

        homePage.ImoveisTotalVendidos = imoveisTabela.Where(i => i.Estado == "Indisponível" && i.TipoPublicidade == 2).Count();
        homePage.ImoveisTotalArrendados = imoveisTabela.Where(i => i.Estado == "Indisponível" && i.TipoPublicidade == 1).Count();

        /*---------------------------------------------------------------------------------------------*/

        homePage.ValorVendas = imoveisTabela.Where(i => i.Estado == "Disponível" && i.TipoPublicidade == 2).Sum(i => i.Preco);
        homePage.ValorArrendamento = imoveisTabela.Where(i => i.Estado == "Disponível" && i.TipoPublicidade == 1).Sum(i => i.Preco);

        return homePage;
    }

    public async Task NotificarClientes(string codigo)
    {
        var clientes = await context.TabelaSolicitacaoCliente.ToListAsync();
        var imovel = await context.TabelaImovel.FirstOrDefaultAsync(i => i.Codigo == codigo);
        if (imovel is null) return;

        var local = await context.TabelaLocalizacao.FirstOrDefaultAsync(i => i.Id == imovel.IdLocalizacao);
        if (local is null) return;
        var pais = await context.TabelaPais.FirstOrDefaultAsync(p => p.Id == local.IdPais); 
        var provincia = await context.TabelaProvincia.FirstOrDefaultAsync(p => p.Id == local.IdProvincia); 
        var municipio = await context.TabelaMunicipio.FirstOrDefaultAsync(p => p.Id == local.IdMunicipio); 
        var bairro = await context.TabelaBairro.FirstOrDefaultAsync(p => p.Id == local.IdBairro); 

        if (imovel is not null)
        {
            foreach (var item in clientes)
            {
                if (item.PrecoMinimo >= imovel.Preco && item.PrecoMaximo <= imovel.Preco || item.PrecoMinimo == 0 || item.PrecoMaximo == 0)
                {
                    if (item.Localizacao == pais!.NomePais || item.Localizacao == provincia!.NomeProvincia || item.Localizacao == municipio!.NomeMunicipio || item.Localizacao == bairro!.NomeBairro)
                    {
                       if(item.IdTipoImovel  == imovel.IdNaturezaImovel || item.IdTipoImovel == 0)
                       {
                             var data = new NotificarCliente()
                            {
                                IdPublicacao = imovel.Codigo,
                                DataNotificacao = imovel.DataSolicitacao,
                                IdSolicitacao = item.Id,
                                Mensagem = imovel.Descricao,                    
                            };
                            await context.TabelaNotificarCliente.AddAsync(data);
                            await context.SaveChangesAsync();
                       }   
                    }                    
                }
            }
        }
    }
}
