using System;
using Microsoft.EntityFrameworkCore;

namespace App_Imobiliaria_api.AppContext;

public class AppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host = localhost; Port = 5432, User Id = PostgresDb; Database = db_imob; Password = dellgsa");
    }

    
}
