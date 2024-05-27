using Microsoft.EntityFrameworkCore;

namespace CoiffeurBuddy.Models
{
	public class Contexto : DbContext
	{
		public Contexto(DbContextOptions<Contexto> options): 
			base(options) { }

		public DbSet<Servico> Servicos { get; set; }

	}
}
