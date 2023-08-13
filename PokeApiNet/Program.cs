using Microsoft.EntityFrameworkCore;
using PokeApiNet.Data;
using PokeApiNet.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Registro do PokeApiClient como serviço
        builder.Services.AddScoped<PokeApiClient>();

        builder.Services.AddDbContext<PokeApiContext>(
            dbContextOptions => dbContextOptions.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

        builder.Services.AddScoped<PokemonRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}