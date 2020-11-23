using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cadastro_de_dev.Models;
using cadastro_de_dev.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace cadastro_de_dev.Controllers
{
    public class DesenvolvedorController : Controller
    {
        OracleParameter cursor = new OracleParameter("cursor_output", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

        private readonly CadastroDeDevContext _context;

        public DesenvolvedorController(CadastroDeDevContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Desenvolvedores.Include(e => e.Empresa).OrderBy(c => c.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            var empresas = _context.Empresas.OrderBy(e => e.NomeEmpresa).ToList();
            empresas.Insert(0, new Empresa()
            {
                EmpresaID = 0,
                NomeEmpresa = "Selecione uma empresa"
            });
            ViewBag.Empresas = empresas;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Email, Telefone, EmpresaID")] Desenvolvedor desenvolvedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(desenvolvedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(desenvolvedor);
        }

        //Get:Desenvolvedor/Edit/id
        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var desenvolvedor = await _context.Desenvolvedores.Include(e => e.Empresa).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }

            var empresas = _context.Empresas.OrderBy(e => e.NomeEmpresa).ToList();
            empresas.Insert(0, new Empresa()
            {
                EmpresaID = 0,
                NomeEmpresa = "Selecione uma empresa"
            });
            ViewBag.Empresas = empresas;

            return View(desenvolvedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DesenvolvedorID, Nome, Email, Telefone, EmpresaID")] Desenvolvedor desenvolvedor)
        {
            if (id != desenvolvedor.DesenvolvedorID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desenvolvedor);
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesenvolvedorExists(desenvolvedor.DesenvolvedorID))
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
            return View(desenvolvedor);
        }

        public bool DesenvolvedorExists(long? id)
        {
            return _context.Desenvolvedores.Any(d => d.DesenvolvedorID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var desenvolvedor = await _context.Desenvolvedores.Include(e => e.Empresa).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }
            return View(desenvolvedor);
        }

        //Get:Desenvolvedor/Delete/id
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var desenvolvedor = await _context.Desenvolvedores.Include(e => e.Empresa).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }
            return View(desenvolvedor);
        }

        //POST:Desenvolvedor/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var desenvolvedor = await _context.Desenvolvedores.Include(e => e.Empresa).SingleOrDefaultAsync(m => m.DesenvolvedorID == id);
            _context.Desenvolvedores.Remove(desenvolvedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowLangs(long? id)
        {
            var linguagem = new List<Linguagem>();

            if (id == null)
            {
                return NotFound();
            }
            var cursor = new OracleParameter("cursor_output", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            //string sql = $"execute LingDeDev(id, :cursor_output);", new object[] { cursor }.ToList();

            linguagem = await _context.Linguagens.FromSqlRaw($"begin LingDeDev({id}, :cursor_output); end;", new object[] { cursor }).ToListAsync();

            var desenvolvedor = await _context.Desenvolvedores.SingleOrDefaultAsync(i => i.DesenvolvedorID == id);
            ViewData["Desenvolvedor"] = desenvolvedor.Nome;

            return View(linguagem);
        }

    }
}
