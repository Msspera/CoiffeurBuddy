using CoiffeurBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoiffeurBuddy.Controllers
{
    public class ConsultaCliente : Controller
    {
        private readonly Contexto contexto;

        public ConsultaCliente(Contexto context)
        {
            contexto = context;
        }

        public IActionResult FiltrarCliente() 
        { 
            return View(); 
        }
        public IActionResult Clientes(string buscacli)
        {
            var ListaCliente = contexto.Clientes.Include(cli => cli.Nome).
                                           Include(cli => cli.Celular).
                                           Include(cli => cli.Email).
                                           Include(cli => cli.Endereco).
                                           Include(cli => cli.Sexo).
                                           Include(cli => cli.Nascimento).
                                           Where(o => o.Nome == buscacli).
                                           ToList();


            return View(ListaCliente);
        }
    }
}
