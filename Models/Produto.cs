using System.ComponentModel.DataAnnotations;

namespace CoiffeurBuddy.Models
{
	public class Produto
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(120)]
		public string Descricao { get; set; }

		public List<Comanda> Comandas { get; set; }

	}
}
