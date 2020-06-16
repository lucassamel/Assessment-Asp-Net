using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment_Asp_Net.Data;
using Assessment_Asp_Net.Models;

namespace Assessment_Asp_Net.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoaContext _context;

        PessoaContext db = new PessoaContext();

        public PessoasController(PessoaContext context)
        {
            _context = context;
        }

        public List<Pessoa> ListaPessoas()
        {
            List<Pessoa> listaPessoas = new List<Pessoa>();
            foreach( var item in db.Pessoas)
            {
                listaPessoas.Add(item);
            }

            
            return listaPessoas;
        }

        public double CalcularAniversario (DateTime niver)
        {
            DateTime data = new DateTime(DateTime.Now.Year, niver.Month, niver.Day);

            if (data < DateTime.Now.Date)
                data = data.AddYears(1);

            double dias = data.Subtract(DateTime.Now.Date).TotalDays;

            return dias;

        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {          
            //foreach (var a in _context.Pessoas)
            //{
            //    DateTime data = new DateTime(DateTime.Now.Year, a.Aniversario.Month, a.Aniversario.Day);

            //    if (data < DateTime.Now.Date)
            //        data = data.AddYears(1);

            //    double dias = data.Subtract(DateTime.Now.Date).TotalDays;

            //    a.Dias = dias;             
            //}

            return View(await _context.Pessoas.OrderBy(x => x.Aniversario)
                .ThenBy(x => x.Aniversario.Month)
                .ThenBy(x => x.Aniversario.Day).ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Aniversario")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Aniversario")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }

        public ActionResult Buscar()
        {
            string pesquisa = "";

            return View(db.Pessoas.Where(x => x.Nome.Contains(pesquisa) || pesquisa == null).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BuscarAsync(string pesquisa)
        {
            try
            {
                return View(await _context.Pessoas.Where(x => x.Nome.Contains(pesquisa) || pesquisa == null).ToListAsync());
            }
            catch
            {

            }

            return View();

        }

        public async Task<IActionResult> AniversariantesToday()
        {
            return View(await _context.Pessoas.ToListAsync());
        }

        
    }
}
