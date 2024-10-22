using API.Models;
using Microsoft.EntityFrameworkCore;

public class AppDataContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; } // Adicionada a DbSet para Folha

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
        modelBuilder.Entity<Folha>().ToTable("Folha"); // Configuração da tabela Folha
    }

    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated(); // Chama para garantir que o banco de dados e tabelas sejam criados
    }
}
