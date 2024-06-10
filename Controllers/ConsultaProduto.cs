using CoiffeurBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoiffeurBuddy.Controllers
{
    public class ConsultaProduto : Controller
    {
        private readonly Contexto contexto;

        public ConsultaProduto(Contexto context)
        {
            contexto = context;
        }

        public IActionResult FiltrarProduto()
        {
            return View();
        }
        public IActionResult Produto(string buscaprod)
        {
            var prodln = contexto.Produtos.Include(prod => prod.Descricao).
                                           Include(prod => prod.Valor).
                                           Include(prod => prod.Estoque).
                                           Where(o => o.Descricao == buscaprod).
                                           ToList();


            return View(prodln);
        }
    }
}
