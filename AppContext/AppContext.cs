using System;
using App_Imobiliaria_api.Models.imovel;
using App_Imobiliaria_api.Models.lead;
using App_Imobiliaria_api.Models.localizacao;
using App_Imobiliaria_api.Models.mensagem;
using App_Imobiliaria_api.Models.usuario;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.AppContext;

public class AppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host = localhost; Port = 5432, User Id = PostgresDb; Database = db_imob; Password = dellgsa");
    }
    /* USUARIO ========================================================================*/
    public DbSet<Funcionario> TabelaFuncionarios { get; set; }
    public DbSet<ClienteProprietario> TabelaClientesProprietarios {get; set; }
    public DbSet<ClienteSolicitante> TabelaClientesSolicitantes {get; set; }
    /* IMOVEL ========================================================================*/
    public DbSet<Imovel> TabelaImovel { get; set; }
    public DbSet<NaturezaImovel> TabelaNaturezaImovel { get; set; }
    public DbSet<TipoImovel> TabelaTipoImovel { get; set; }
    public DbSet<Foto> TabelaFoto { get; set; }
    public DbSet<Publicacao> TabelaPublicacao { get; set; }
    /* LOCALIZAÇÃO ========================================================================*/
    public DbSet<Pais> TabelaPais { get; set; }
    public DbSet<Provincia> TabelaProvincia { get; set; }
    public DbSet<Municipio> TabelaMunicipio { get; set; }
    public DbSet<Bairro> TabelaBairro { get; set; }
    public DbSet<Rua> TabelaRua { get; set; }
    public DbSet<Localizacao> TabelaLocalizacao { get; set; }
    /* LEAD ========================================================================*/
    public DbSet<Lead> TabelaLead { get; set; }
    /* MENSAGEM ========================================================================*/
    public DbSet<SolicitacaoCliente> TabelaSolicitacaoCliente { get; set; }
    public DbSet<NotificarProprietario> TabelaNotificarProprietario { get; set; }
    public DbSet<NotificarCliente> TabelaNotificarCliente { get; set; }
    public DbSet<Chat> TabelaChat { get; set; }
    /* ========================================================================*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>().Property(f => f.Estado).HasConversion<string>();
        modelBuilder.Entity<ClienteProprietario>().Property(c => c.Estado).HasConversion<string>();
        modelBuilder.Entity<ClienteSolicitante>().Property(c => c.Estado).HasConversion<string>();
        /* =======================================================================================*/
        modelBuilder.Entity<Imovel>().Property(c => c.Estado).HasConversion<string>();
        modelBuilder.Entity<Imovel>().Property(c => c.TipoPublicidade).HasConversion<string>();
        /* =======================================================================================*/
        modelBuilder.Entity<Lead>().Property(l => l.Estado).HasConversion<string>();
    }
}
