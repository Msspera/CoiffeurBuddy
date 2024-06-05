using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoiffeurBuddy.Models
{
	public class Comanda
	{
		[Key]
		public int Id { get; set; }

		public int AtendimentoId { get; set; }
		[ForeignKey("AtendimentoId")]
		public Atendimento Atendimento { get; set; }

		[Required]
		public float ValorTotal { get; set; }

		[Required]
		public char MetodoPagamento { get; set; }

		public ICollection<ComandaProduto> ComandaProdutos { get; set; }
	}
}
