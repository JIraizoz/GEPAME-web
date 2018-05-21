using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GEPAME.Models;
using Microsoft.AspNetCore.Authorization;

namespace GEPAME.Controllers
{
    [Authorize]
    public class TypeVehiclesController : Controller
    {
        private readonly GEPAMEContext _context;

        public TypeVehiclesController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: TypeVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoVehiculo.ToListAsync());
        }

        // GET: TypeVehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo
                .SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // GET: TypeVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descripcion")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVehiculo);
        }

        // GET: TypeVehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TypeVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Descripcion")] TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVehiculoExists(tipoVehiculo.Codigo))
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
            return View(tipoVehiculo);
        }

        // GET: TypeVehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo
                .SingleOrDefaultAsync(m => m.Codigo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // POST: TypeVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.Codigo == id);
            _context.TipoVehiculo.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVehiculoExists(string id)
        {
            return _context.TipoVehiculo.Any(e => e.Codigo == id);
        }
    }
}
