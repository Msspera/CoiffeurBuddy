using CoiffeurBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoiffeurBuddy.Controllers
{
    
    public class ConsultaServicos : Controller
    {
        private readonly Contexto contexto;

        public ConsultaServicos(Contexto context)
        {
            contexto = context;
        }
        public IActionResult Index()
        {
            var servln = contexto.Servicos.Include(serv => serv.Descricao).
                                           Include(servalor => servalor.Valor).
                                           Where(o => o.Descricao.Contains("Corte")).
                                           ToList();


            return View(servln);
        }
    }
}
