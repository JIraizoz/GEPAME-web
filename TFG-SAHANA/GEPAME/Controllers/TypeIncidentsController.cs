using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GEPAME.Models;

namespace GEPAME.Controllers
{
    public class TypeIncidentsController : Controller
    {
        private readonly GEPAMEContext _context;

        public TypeIncidentsController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: TypeIncidents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoIncidencia.ToListAsync());
        }

        // GET: TypeIncidents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIncidencia = await _context.TipoIncidencia
                .SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoIncidencia == null)
            {
                return NotFound();
            }

            return View(tipoIncidencia);
        }

        // GET: TypeIncidents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeIncidents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descripcion")] TipoIncidencia tipoIncidencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoIncidencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoIncidencia);
        }

        // GET: TypeIncidents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIncidencia = await _context.TipoIncidencia.SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoIncidencia == null)
            {
                return NotFound();
            }
            return View(tipoIncidencia);
        }

        // POST: TypeIncidents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Descripcion")] TipoIncidencia tipoIncidencia)
        {
            if (id != tipoIncidencia.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoIncidencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoIncidenciaExists(tipoIncidencia.Codigo))
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
            return View(tipoIncidencia);
        }

        // GET: TypeIncidents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIncidencia = await _context.TipoIncidencia
                .SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoIncidencia == null)
            {
                return NotFound();
            }

            return View(tipoIncidencia);
        }

        // POST: TypeIncidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tipoIncidencia = await _context.TipoIncidencia.SingleOrDefaultAsync(m => m.Codigo == id);
            _context.TipoIncidencia.Remove(tipoIncidencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoIncidenciaExists(string id)
        {
            return _context.TipoIncidencia.Any(e => e.Codigo == id);
        }
    }
}
