﻿namespace CoiffeurBuddy.ViewModels
{
	public class ComandaViewModel
	{
		public int Id { get; set; }
		public int AtendimentoId { get; set; }
		public float ValorTotal { get; set; }
		public char MetodoPagamento { get; set; }
		public List<ComandaProdutoViewModel> ComandaProdutos { get; set; }
	}
}
