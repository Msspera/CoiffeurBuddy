using Microsoft.EntityFrameworkCore;
using CoiffeurBuddy.Models;

namespace CoiffeurBuddy.Models
{
	public class Contexto : DbContext
	{
		public Contexto(DbContextOptions<Contexto> options): 
			base(options) { }

		public DbSet<Servico> Servicos { get; set; }
		public DbSet<Funcionario> Funcionarios { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Atendimento> Atendimentos { get; set; }

		public DbSet<Comanda> Comandas { get; set; }
		public DbSet<Produto> Produtos { get; set; }
		public DbSet<ComandaProduto> ComandaProdutos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configuração das entidades e relacionamentos
			modelBuilder.Entity<ComandaProduto>()
				.HasKey(cp => new { cp.ComandaId, cp.ProdutoId });

			modelBuilder.Entity<ComandaProduto>()
				.HasOne(cp => cp.Comanda)
				.WithMany(c => c.ComandaProdutos)
				.HasForeignKey(cp => cp.ComandaId);

			modelBuilder.Entity<ComandaProduto>()
				.HasOne(cp => cp.Produto)
				.WithMany(p => p.ComandaProdutos)
				.HasForeignKey(cp => cp.ProdutoId);
		}

	}
}
