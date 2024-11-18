using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Repository.Interfaces.imovelInterface;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using App_Imobiliaria_api.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ImobContext>(option => option.UseNpgsql(connectionString));
builder.Services.AddTransient<IGerente, GerenteService>();   
builder.Services.AddTransient<IUsuario, UsuarioService>();
builder.Services.AddTransient<IClienteProprietario, ProprietarioService>();
builder.Services.AddTransient<IImovel, ImovelService>();
builder.Services.AddTransient<IClienteSolicitante, ClienteService>(); 
 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configurar middleware para servir arquivos de um diretório externo
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider("/home/ckm/Imagens/StorageYula"), // Caminho externo no linux
    RequestPath = "/images", // Caminho acessível via URL
    ServeUnknownFileTypes = false, // Não servir tipos desconhecidos
    DefaultContentType = "image/jpeg", // Conteúdo padrão caso a extensão não seja reconhecida
    OnPrepareResponse = ctx =>
    {
        // Bloquear acesso a arquivos sem extensões de imagem
        if (!ctx.File.Name.EndsWith(".jpg") && !ctx.File.Name.EndsWith(".png"))
        {
            ctx.Context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
    }
});
# if !DEBUG
app.UseHttpsRedirection();
#endif
app.UseAuthorization();

app.MapControllers();

app.Run();
