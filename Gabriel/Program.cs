using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext para usar SQLite
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite($"Data Source=pedro_gabriel.db"));

var app = builder.Build();

// Crie o banco de dados e as tabelas se não existirem
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();
    dbContext.EnsureDatabaseCreated();
}

// Definição das rotas para Funcionários
app.MapPost("/api/funcionario/cadastrar", async (Funcionario funcionario, AppDataContext dbContext) =>
{
    dbContext.Funcionarios.Add(funcionario);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/funcionario/{funcionario.FuncionarioId}", funcionario);
});

app.MapGet("/api/funcionario/listar", async (AppDataContext dbContext) =>
{
    return await dbContext.Funcionarios.ToListAsync();
});

// Definição das rotas para Folha
app.MapPost("/api/folha/cadastrar", async (Folha folha, AppDataContext dbContext) =>
{
    dbContext.Folhas.Add(folha);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/folha/{folha.FolhaId}", folha);
});

app.MapGet("/api/folha/listar", async (AppDataContext dbContext) =>
{
    return await dbContext.Folhas.ToListAsync();
});

// Adicione a rota para buscar uma folha específica
app.MapGet("/folha/buscar/{FolhaId}", async (int FolhaId, AppDataContext dbContext) =>
{
    try
    {
        var folha = await dbContext.Folhas
            .Include(f => f.Funcionario) // Inclui os dados do funcionário associado
            .FirstOrDefaultAsync(f => f.FolhaId == FolhaId); // Filtra pela FolhaId

        if (folha is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(folha);
    }
    catch (Exception ex)
    {
        return Results.Problem("Ocorreu um erro ao buscar a folha: " + ex.Message);
    }
});



app.Run();
