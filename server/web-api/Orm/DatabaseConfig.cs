using eAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApi.Orm;

public static class DatabaseOperations
{
    // Aplica as migrações pendentes no banco de dados ao iniciar a aplicação.
    public static void ApplyMigrations(this IHost app)
    {
        var scope = app.Services.CreateScope(); // Cria um escopo para obter serviços com tempo de vida limitado

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Obtém o DbContext do escopo

        dbContext.Database.Migrate();
    }
}
