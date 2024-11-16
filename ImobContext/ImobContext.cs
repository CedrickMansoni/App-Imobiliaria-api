using System;
using App_Imobiliaria_api.Models.DropBox;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.lead;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.ImobContext;

public class ImobContext(DbContextOptions<ImobContext> options) : DbContext(options)
{
    
    /* USUARIO ========================================================================*/
    public required DbSet<Funcionario> TabelaFuncionarios { get; set; } 
    public required DbSet<ClienteProprietario> TabelaClientesProprietarios {get; set; }
    public required DbSet<ClienteSolicitante> TabelaClientesSolicitantes {get; set; }
    /* IMOVEL ========================================================================*/
    public required DbSet<Imovel> TabelaImovel { get; set; }
    public required DbSet<NaturezaImovel> TabelaNaturezaImovel { get; set; }
    public required DbSet<TipoImovel> TabelaTipoImovel { get; set; }
    public required DbSet<Foto> TabelaFoto { get; set; }
    public required DbSet<Publicacao> TabelaPublicacao { get; set; }
    public required DbSet<TipoPublicacao> TabelaTipoPublicacao { get; set; }
    /* LOCALIZAÇÃO ========================================================================*/
    public DbSet<Pais>? TabelaPais { get; set; }
    public required DbSet<Provincia> TabelaProvincia { get; set; }
    public required DbSet<Municipio> TabelaMunicipio { get; set; }
    public required DbSet<Bairro> TabelaBairro { get; set; }
    public required DbSet<Rua> TabelaRua { get; set; }
    public required DbSet<Localizacao> TabelaLocalizacao { get; set; }
    /* LEAD ========================================================================*/
    public required DbSet<Lead> TabelaLead { get; set; }
    /* MENSAGEM ========================================================================*/
    public required DbSet<SolicitacaoCliente> TabelaSolicitacaoCliente { get; set; }
    public required DbSet<NotificarProprietario> TabelaNotificarProprietario { get; set; }
    public required DbSet<NotificarCliente> TabelaNotificarCliente { get; set; }
    public required DbSet<Chat> TabelaChat { get; set; }
    /* TABELA DOS FAVORITOS ==========================================================*/
    public required DbSet<Favorito> TabelaFavorito {get; set;}
    /* TOKEN DROPBOX ==========================================================*/
    public required DbSet<Token> TabelaToken {get; set;}
    /* ========================================================================*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>().Property(f => f.Estado).HasConversion<string>();
        modelBuilder.Entity<Funcionario>().Property(f => f.Nivel).HasConversion<string>();
        modelBuilder.Entity<ClienteProprietario>().Property(c => c.Estado).HasConversion<string>();
        modelBuilder.Entity<ClienteSolicitante>().Property(c => c.Estado).HasConversion<string>();
        /* =======================================================================================*/
        modelBuilder.Entity<Imovel>().Property(c => c.Estado).HasConversion<string>();
        /* =======================================================================================*/
        modelBuilder.Entity<Lead>().Property(l => l.Estado).HasConversion<string>();
    }
}
