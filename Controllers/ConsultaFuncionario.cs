using CoiffeurBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoiffeurBuddy.Controllers
{
    public class ConsultaFuncionario : Controller
    {
        private readonly Contexto contexto;

        public ConsultaFuncionario(Contexto context)
        {
            contexto = context;
        }
        public IActionResult Index()
        {
            var funcln = contexto.Funcionarios.Include(func => func.Nome).
                                           Include(func => func.Funcao).
                                           Include(func => func.Celular).
                                           Include(func => func.Email).
                                           Include(func => func.Endereco).
                                           Where(o => o.Nome.Contains("Fulano")).
                                           ToList();


            return View(funcln);
        }
    }
}

