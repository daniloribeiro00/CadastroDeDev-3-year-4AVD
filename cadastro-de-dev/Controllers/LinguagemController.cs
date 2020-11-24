using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro_de_dev.Data;
using cadastro_de_dev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace cadastro_de_dev.Controllers
{
    public class LinguagemController : Controller
    {
        private readonly CadastroDeDevContext _context;

        public LinguagemController(CadastroDeDevContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Linguagens.OrderBy(c => c.NomeLinguagem).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeLinguagem")] Linguagem linguagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(linguagem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(linguagem);
        }

        //Get:Empresa/Edit/id
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var linguagem = await _context.Linguagens.SingleOrDefaultAsync(m => m.LinguagemID == id);
            if (linguagem == null)
            {
                return NotFound();
            }
            return View(linguagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("LinguagemID, NomeLinguagem")] Linguagem linguagem)
        {
            if (id != linguagem.LinguagemID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linguagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinguagemExists(linguagem.LinguagemID))
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
            return View(linguagem);
        }

        public bool LinguagemExists(long? id)
        {
            return _context.Linguagens.Any(e => e.LinguagemID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var linguagem = await _context.Linguagens.SingleOrDefaultAsync(m => m.LinguagemID == id);
            if (linguagem == null)
            {
                return NotFound();
            }
            return View(linguagem);
        }

        //Get:Empresa/Delete/id
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var linguagem = await _context.Linguagens.SingleOrDefaultAsync(m => m.LinguagemID == id);
            if (linguagem == null)
            {
                return NotFound();
            }
            return View(linguagem);
        }

        //POST:Linguagem/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var linguagem = await _context.Linguagens.SingleOrDefaultAsync(m => m.LinguagemID == id);
            _context.Linguagens.Remove(linguagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}