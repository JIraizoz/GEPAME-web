using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GEPAME.Models;
using GEPAME.AppCode.LN;

namespace GEPAME.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly GEPAMEContext _context;

        public IncidentsController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: Incidents
        public async Task<IActionResult> Index()
        {
            var gEPAMEContext = _context.Incidencia.Include(i => i.TipoIncidenciaNavigation);
            return View(await gEPAMEContext.ToListAsync());
        }

        public async Task<IActionResult> Map()
        {
            var gEPAMEContext = _context.Incidencia.Include(i => i.TipoIncidenciaNavigation);
            return View(await gEPAMEContext.ToListAsync());
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(string id, string tipoIncidencia)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.Incidencia
                .Include(i => i.TipoIncidenciaNavigation)
                .SingleOrDefaultAsync(m => m.TipoIncidencia == tipoIncidencia && m.IdIncidencia == id);
            if (incidencia == null)
            {
                return NotFound();
            }

            return View(incidencia);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            ViewData["TipoIncidencia"] = new SelectList(_context.TipoIncidencia, "Codigo", "Codigo");
            //    var posicion = new LN_Utilidades().GetPosicionIncidencia(_context);
            //ViewData["Posicion"] = posicion;
            ViewData["SiguienteID"] = GetSiguienteID(_context.TipoIncidencia.First().Codigo);
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoIncidencia,IdIncidencia,Utm,Fecha,Estado,Descripcion")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoIncidencia"] = new SelectList(_context.TipoIncidencia, "Codigo", "Codigo", incidencia.TipoIncidencia);
            return View(incidencia);
        }

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(string id, string tipoIncidencia)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.Incidencia.SingleOrDefaultAsync(m => m.TipoIncidencia == tipoIncidencia && m.IdIncidencia == id);
            if (incidencia == null)
            {
                return NotFound();
            }
            ViewData["TipoIncidencia"] = new SelectList(_context.TipoIncidencia, "Codigo", "Codigo", incidencia.TipoIncidencia);
            return View(incidencia);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string tipoIncidencia, [Bind("TipoIncidencia,IdIncidencia,Utm,Fecha,Estado,Descripcion")] Incidencia incidencia)
        {
            if (tipoIncidencia != incidencia.TipoIncidencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaExists(incidencia.IdIncidencia, incidencia.TipoIncidencia))
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
            ViewData["TipoIncidencia"] = new SelectList(_context.TipoIncidencia, "Codigo", "Codigo", incidencia.TipoIncidencia);
            return View(incidencia);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(string id, string tipoIncidencia)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.Incidencia
                .Include(i => i.TipoIncidenciaNavigation)
                .SingleOrDefaultAsync(m => m.TipoIncidencia == tipoIncidencia && m.IdIncidencia == id);
            if (incidencia == null)
            {
                return NotFound();
            }

            return View(incidencia);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string tipoIncidencia)
        {
            var incidencia = await _context.Incidencia.SingleOrDefaultAsync(m => m.TipoIncidencia == tipoIncidencia && m.IdIncidencia == id);
            _context.Incidencia.Remove(incidencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidenciaExists(string id, string tipoIncidencia)
        {
            return _context.Incidencia.Any(e => e.TipoIncidencia == tipoIncidencia && e.IdIncidencia == id);
        }

        private string GetSiguienteID(string tipoIncidencia)
        {
            string id = "";

            id = new LN_Utilidades().GetSiguienteIDIncidencia(_context, tipoIncidencia);

            return id;
        }
    }
}
