using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoiffeurBuddy.Models
{
	[Table("Servicos")]
	public class Servico
	{
		[Key] //anotação chave primária
		public int Id { get; set; }

		[Required]
		[StringLength(90)]
		public string Descricao	{ get; set; }
		[Required]
		public float Valor {  get; set; }
	}
}
