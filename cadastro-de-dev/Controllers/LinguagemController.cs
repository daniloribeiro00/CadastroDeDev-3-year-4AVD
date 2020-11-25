using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
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

                    //----------------------------------TENTAMOS, MAS NÃO CONSEGUIMOS...-------------------------- 

                    //var conn = new OracleConnection("User Id=CADDEVDB;Password=CADDEVDB;Data Source=127.0.0.1:1521/xe;");

                    //conn.Open();
                    //OracleCommand cmd = new OracleCommand();
                    //cmd.Connection = conn;
                    //cmd.CommandText = "LingEdit";

                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //OdbcParameter param = new OdbcParameter();

                    //cmd.Parameters.Add("lingid", OracleDbType.Int32).Value = Convert.ToInt32(id);
                    //cmd.Parameters.Add("lingname", OracleDbType.NVarchar2).Value = "C sharp";
                    ////cmd.Parameters.Add("mensagem", OracleDbType.Varchar2, System.Data.ParameterDirection.ReturnValue);

                    //OracleParameter mensagem = (new OracleParameter("mensagem", OracleDbType.Varchar2, 30));
                    //mensagem.Direction = System.Data.ParameterDirection.ReturnValue;
                    //cmd.Parameters.Add(mensagem);

                    ////cmd.Parameters.Add("lingname", OracleDbType.Varchar2);
                    ////cmd.Parameters.Add("mensagem", OracleDbType.Varchar2);
                    ////cmd.Parameters["mensagem"].Direction = System.Data.ParameterDirection.ReturnValue;

                    //cmd.ExecuteNonQuery();
                    ////var mensagem = Convert.ToString(cmd.Parameters["mensagem"].Value);

                    //cmd.Parameters.RemoveAt(0);
                    ////var mensagem = Convert.ToString(cmd.Parameters["mensagem"].Value);

                    //cmd.Connection.Close();

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