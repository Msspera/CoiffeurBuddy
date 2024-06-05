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

		[Required]
		public float Valor { get; set; }

		[Required]
        public int Estoque { get; set; }

		public ICollection<ComandaProduto> ComandaProdutos { get; set; }

	}
}
