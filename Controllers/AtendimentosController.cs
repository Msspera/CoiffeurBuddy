﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoiffeurBuddy.Models;

namespace CoiffeurBuddy.Controllers
{
    public class AtendimentosController : Controller
    {
        private readonly Contexto _context;

        public AtendimentosController(Contexto context)
        {
            _context = context;
        }

        // GET: Atendimentos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Atendimento.Include(a => a.Cliente).Include(a => a.Funcionario).Include(a => a.Servico);
            return View(await contexto.ToListAsync());
        }

        // GET: Atendimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimento
                .Include(a => a.Cliente)
                .Include(a => a.Funcionario)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // GET: Atendimentos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Celular");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Celular");
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Descricao");
            return View();
        }

        // POST: Atendimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServicoId,ClienteId,FuncionarioId,DataHora")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atendimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Celular", atendimento.ClienteId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Celular", atendimento.FuncionarioId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Descricao", atendimento.ServicoId);
            return View(atendimento);
        }

        // GET: Atendimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimento.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Celular", atendimento.ClienteId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Celular", atendimento.FuncionarioId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Descricao", atendimento.ServicoId);
            return View(atendimento);
        }

        // POST: Atendimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServicoId,ClienteId,FuncionarioId,DataHora")] Atendimento atendimento)
        {
            if (id != atendimento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimento.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Celular", atendimento.ClienteId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Celular", atendimento.FuncionarioId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Descricao", atendimento.ServicoId);
            return View(atendimento);
        }

        // GET: Atendimentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimento
                .Include(a => a.Cliente)
                .Include(a => a.Funcionario)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // POST: Atendimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atendimento = await _context.Atendimento.FindAsync(id);
            if (atendimento != null)
            {
                _context.Atendimento.Remove(atendimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtendimentoExists(int id)
        {
            return _context.Atendimento.Any(e => e.Id == id);
        }
    }
}