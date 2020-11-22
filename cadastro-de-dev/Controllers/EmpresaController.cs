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
    public class EmpresaController : Controller
    {
        private readonly CadastroDeDevContext _context;

        public EmpresaController(CadastroDeDevContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.OrderBy(c => c.NomeEmpresa).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeEmpresa")] Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(empresa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(empresa);
        }

        //Get:Empresa/Edit/id
        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaID == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("EmpresaID, NomeEmpresa")] Empresa empresa)
        {
            if (id != empresa.EmpresaID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.EmpresaID))
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
            return View(empresa);
        }

        public bool EmpresaExists(long? id)
        {
            return _context.Empresas.Any(e => e.EmpresaID == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaID == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        //Get:Empresa/Delete/id
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaID == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        //POST:Empresa/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var empresa = await _context.Empresas.SingleOrDefaultAsync(m => m.EmpresaID == id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ShowDevs(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //string sql = $"execute DevDeInst({id});";
            var desenvolvedor = await _context.Desenvolvedores.FromSqlInterpolated($"begin DevDeInst({id}); end;").ToListAsync();
            var empresa = await _context.Empresas.SingleOrDefaultAsync(i => i.EmpresaID == id);
            ViewData["Empresa"] = empresa.NomeEmpresa;

            return View(desenvolvedor);
        }

    }
}