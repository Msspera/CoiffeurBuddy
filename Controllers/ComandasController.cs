using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoiffeurBuddy.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CoiffeurBuddy.ViewModels;

namespace CoiffeurBuddy.Controllers
{
    public class ComandasController : Controller
    {
        private readonly Contexto _context;

        public ComandasController(Contexto context)
        {
            _context = context;
        }

        // GET: Comandas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Comandas.Include(c => c.Atendimento);
            return View(await contexto.ToListAsync());
        }

        // GET: Comandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var comanda = await _context.Comandas
								.Include(c => c.ComandaProdutos)
								.ThenInclude(cp => cp.Produto)
								.FirstOrDefaultAsync(m => m.Id == id);
			if (comanda == null)
			{
				return NotFound();
			}
			var produtos = await _context.Produtos.ToListAsync();
			ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", comanda.AtendimentoId);
			ViewData["Produtos"] = produtos;

			var comandaViewModel = new ComandaViewModel
			{
				Id = comanda.Id,
				AtendimentoId = comanda.AtendimentoId,
				ValorTotal = comanda.ValorTotal,
				MetodoPagamento = comanda.MetodoPagamento,
				ComandaProdutos = produtos.Select(p => new ComandaProdutoViewModel
				{
					ProdutoId = p.Id,
					Quantidade = comanda.ComandaProdutos.FirstOrDefault(cp => cp.ProdutoId == p.Id)?.Quantidade ?? 0
				}).ToList()
			};

			return View(comandaViewModel);
        }

        // GET: Comandas/Create
        public IActionResult Create()
        {
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id");
			ViewData["Produtos"] = _context.Produtos.ToList();
			return View();
        }

        // POST: Comandas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int AtendimentoId, char MetodoPagamento, List<ComandaProdutoViewModel> comandaProdutos)
        {
            if (ModelState.IsValid)
            {
				float valorAtendimento = ObterValorAtendimento(AtendimentoId);
				float gastoProdutos = CalcularTotalGastoProdutos(comandaProdutos);
				float ValorTotal = valorAtendimento + gastoProdutos;
				
				var comanda = new Comanda
				{
					AtendimentoId = AtendimentoId,
					ValorTotal = ValorTotal,
					MetodoPagamento = MetodoPagamento,
					ComandaProdutos = comandaProdutos
					.Where(cp => cp.Quantidade > 0)
					.Select(cp => new ComandaProduto
					{
						ProdutoId = cp.ProdutoId,
						Quantidade = cp.Quantidade
					}).ToList()
				};
				_context.Add(comanda);
				await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			// ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", comanda.AtendimentoId);
			//ViewData["Produtos"] = _context.Produtos.ToList();
			return View();
        }
		private float ObterValorAtendimento(int atendimentoId)
		{
			// Consulta o atendimento no banco de dados pelo ID fornecido
			var atendimento = _context.Atendimentos
									 .AsNoTracking() // AsNoTracking() evita que o Entity Framework rastreie as entidades retornadas pela consulta
									 .FirstOrDefault(a => a.Id == atendimentoId);

			// Verifica se o atendimento foi encontrado
			if (atendimento != null)
			{
				var servico = _context.Servicos
								.AsNoTracking()
								.FirstOrDefault(s => s.Id == atendimento.ServicoId);
				// Retorna o valor do atendimento encontrado
				return servico.Valor; 
			}
			else
			{
				return 0; // Valor padrão caso o atendimento não seja encontrado
			}
		}
		private float CalcularTotalGastoProdutos(List<ComandaProdutoViewModel> comandaProdutos)
		{
			float total = 0;
			foreach (var cp in comandaProdutos)
			{
				Produto produto = _context.Produtos
									.AsNoTracking()
									.FirstOrDefault(p => p.Id == cp.ProdutoId);
				total += produto.Valor * cp.Quantidade;
			}
			return total;
		}
		// GET: Comandas/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comandas
								.Include(c => c.ComandaProdutos)
								.ThenInclude(cp => cp.Produto)
								.FirstOrDefaultAsync(m => m.Id == id);
			if (comanda == null)
            {
                return NotFound();
            }
			var produtos = await _context.Produtos.ToListAsync();
			ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", comanda.AtendimentoId);
			ViewData["Produtos"] = produtos;

			var comandaViewModel = new ComandaViewModel
			{
				Id = comanda.Id,
				AtendimentoId = comanda.AtendimentoId,
				ValorTotal = comanda.ValorTotal,
				MetodoPagamento = comanda.MetodoPagamento,
				ComandaProdutos = produtos.Select(p => new ComandaProdutoViewModel
				{
					ProdutoId = p.Id,
					Quantidade = comanda.ComandaProdutos.FirstOrDefault(cp => cp.ProdutoId == p.Id)?.Quantidade ?? 0
				}).ToList()
			};

			return View(comandaViewModel);
        }

        // POST: Comandas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComandaViewModel comandaViewModel)
        {
			if (id != comandaViewModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
            {
                try
                {
					var comanda = await _context.Comandas
					.Include(c => c.ComandaProdutos)
					.FirstOrDefaultAsync(m => m.Id == id);

					var comandaProdutos = comandaViewModel.ComandaProdutos;

					int AtendimentoId = comandaViewModel.AtendimentoId;
					comanda.AtendimentoId = AtendimentoId;
					
					float valorAtendimento = ObterValorAtendimento(AtendimentoId);
					float gastoProdutos = CalcularTotalGastoProdutos(comandaProdutos);
					float ValorTotal = valorAtendimento + gastoProdutos;

					comanda.ValorTotal = ValorTotal;
					comanda.MetodoPagamento = comandaViewModel.MetodoPagamento;

					foreach (var cp in comandaViewModel.ComandaProdutos)
					{
						var comandaProduto = comanda.ComandaProdutos.FirstOrDefault(p => p.ProdutoId == cp.ProdutoId);
						if (comandaProduto != null)
						{
							comandaProduto.Quantidade = cp.Quantidade;
						}
						else
						{
							comanda.ComandaProdutos.Add(new Models.ComandaProduto
							{
								ComandaId = comanda.Id,
								ProdutoId = cp.ProdutoId,
								Quantidade = cp.Quantidade
							});
						}
					}
					

					_context.Update(comanda);
					await _context.SaveChangesAsync();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComandaExists(comandaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", comandaViewModel.AtendimentoId);
            return View(comandaViewModel);
        }

        // GET: Comandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comandas
                .Include(c => c.Atendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // POST: Comandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comanda = await _context.Comandas.FindAsync(id);
            if (comanda != null)
            {
                _context.Comandas.Remove(comanda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComandaExists(int id)
        {
            return _context.Comandas.Any(e => e.Id == id);
        }
    }
}
