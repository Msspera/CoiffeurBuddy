namespace CoiffeurBuddy.Models
{
	public class ComandaProduto
	{
		public int ComandaId { get; set; }
		public Comanda Comanda { get; set; }

		public int ProdutoId { get; set; }
		public Produto Produto { get; set; }

		public int Quantidade { get; set; }
	}
}
