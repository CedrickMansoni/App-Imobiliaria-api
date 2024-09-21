using App_Imobiliaria_api.ImobContext;
using App_Imobiliaria_api.Repository.Interfaces.usuarioInterface;
using App_Imobiliaria_api.Repository.Services;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
# if !DEBUG
app.UseHttpsRedirection();
#endif
app.UseAuthorization();

app.MapControllers();

app.Run();
