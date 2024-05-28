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

	}
}
