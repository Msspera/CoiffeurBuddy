using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoiffeurBuddy.Models
{
	[Table("Funcionarios")]
	public class Funcionario
	{
		[Key] //anotação chave primária
		public int Id { get; set; }

		[Required]
		[StringLength(90)]
		public string Nome { get; set; }

		[Required]
		[StringLength(90)]
		public string Funcao { get; set; }

		[Required]
		[StringLength(11)]
		public string Celular { get; set; }

		[Required]
		[StringLength(120)]
		public string Email { get; set; }

		[Required]
		[StringLength(120)]
		public string Endereco { get; set; }

		[Required]
		public float Salario { get; set; }

		[InverseProperty("Atendimento")]
		public List<Atendimento> Atendimentos { get; set; }
	}
}
