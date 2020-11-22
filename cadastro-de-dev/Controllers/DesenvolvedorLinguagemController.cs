using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastro_de_dev.Data;
using cadastro_de_dev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cadastro_de_dev.Controllers
{
    public class DesenvolvedorLinguagemController : Controller
    {
        private readonly CadastroDeDevContext _context;

        public DesenvolvedorLinguagemController(CadastroDeDevContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DesenvolvedorLinguagens.Include(d => d.Desenvolvedor).Include(l => l.Linguagem).OrderBy(c => c.Desenvolvedor.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            var linguagens = _context.Linguagens.OrderBy(l => l.NomeLinguagem).ToList();
            linguagens.Insert(0, new Linguagem()
            {
                LinguagemID = 0,
                NomeLinguagem = "Selecione uma linguagem"
            });
            ViewBag.Linguagens = linguagens;

            var desenvolvedores = _context.Desenvolvedores.OrderBy(d => d.Nome).ToList();
            desenvolvedores.Insert(0, new Desenvolvedor()
            {
                DesenvolvedorID = 0,
                Nome = "Selecione um desenvolvedor"
            });
            ViewBag.Desenvolvedores = desenvolvedores;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesenvolvedorID, LinguagemID")] DesenvolvedorLinguagem desenvolvedorLinguagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(desenvolvedorLinguagem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(desenvolvedorLinguagem);
        }

        //Get:Desenvolvedor/Edit/id
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var desenvolvedorLinguagem = await _context.DesenvolvedorLinguagens.Include(d => d.Desenvolvedor).Include(l => l.Linguagem).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedorLinguagem == null)
            {
                return NotFound();
            }

            var linguagens = _context.Linguagens.OrderBy(l => l.NomeLinguagem).ToList();
            linguagens.Insert(0, new Linguagem()
            {
                LinguagemID = 0,
                NomeLinguagem = "Selecione uma linguagem"
            });
            ViewBag.Linguagens = linguagens;

            var desenvolvedores = _context.Desenvolvedores.OrderBy(d => d.Nome).ToList();
            desenvolvedores.Insert(0, new Desenvolvedor()
            {
                DesenvolvedorID = 0,
                Nome = "Selecione um desenvolvedor"
            });
            ViewBag.Desenvolvedores = desenvolvedores;

            return View(desenvolvedorLinguagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DesenvolvedorID, LinguagemID")] DesenvolvedorLinguagem desenvolvedorLinguagem)
        {
            if (id != desenvolvedorLinguagem.DesenvolvedorID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desenvolvedorLinguagem);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!DesenvolvedorLinguagemExists(desenvolvedorLinguagem.DesenvolvedorID))
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
            return View(desenvolvedorLinguagem);
        }

        public bool DesenvolvedorLinguagemExists(long? id)
        {
            return _context.DesenvolvedorLinguagens.Any(d => d.DesenvolvedorID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var desenvolvedorLinguagem = await _context.DesenvolvedorLinguagens.Include(d => d.Desenvolvedor).Include(l => l.Linguagem).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedorLinguagem == null)
            {
                return NotFound();
            }
            return View(desenvolvedorLinguagem);
        }

        //Get:DesenvolvedorLinguagem/Delete/id
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var desenvolvedorLinguagem = await _context.DesenvolvedorLinguagens.Include(d => d.Desenvolvedor).Include(l => l.Linguagem).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedorLinguagem == null)
            {
                return NotFound();
            }
            return View(desenvolvedorLinguagem);
        }

        //POST:DesenvolvedorLinguagem/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var desenvolvedorLinguagem = await _context.DesenvolvedorLinguagens.Include(d => d.Desenvolvedor).Include(l => l.Linguagem).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            _context.DesenvolvedorLinguagens.Remove(desenvolvedorLinguagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
