using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoiffeurBuddy.Models
{
	[Table("Atendimentos")]
	public class Atendimento
	{
		[Key]
		public int Id { get; set; }

        public int ServicoId { get; set; }
		[ForeignKey("ServicoId")]
		public Servico Servico { get; set; }

		public int ClienteId { get; set; }
		[ForeignKey("ClienteId")]
		public Cliente Cliente { get; set; }

		public int FuncionarioId { get; set; }
		[ForeignKey("FuncionarioId")]
		public Funcionario Funcionario { get; set; }

		[Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }
    }
}
