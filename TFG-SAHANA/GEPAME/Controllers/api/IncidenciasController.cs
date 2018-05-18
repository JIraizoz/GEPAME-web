using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GEPAME.Models;

namespace GEPAME.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Incidencias")]
    public class IncidenciasController : Controller
    {
        private readonly GEPAMEContext _context;

        public IncidenciasController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: api/Incidencias
        [HttpGet]
        public IEnumerable<Incidencia> GetIncidencia()
        {
            return _context.Incidencia;
        }

        // GET: api/Incidencias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncidencia([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incidencia = await _context.Incidencia.SingleOrDefaultAsync(m => m.TipoIncidencia == id);

            if (incidencia == null)
            {
                return NotFound();
            }

            return Ok(incidencia);
        }

        // POST: api/Incidencias
        [HttpPost]
        public async Task<IActionResult> PostIncidencia([FromBody] Incidencia incidencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Incidencia.Add(incidencia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IncidenciaExists(incidencia.TipoIncidencia))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncidencia", new { id = incidencia.TipoIncidencia }, incidencia);
        }

        private bool IncidenciaExists(string id)
        {
            return _context.Incidencia.Any(e => e.TipoIncidencia == id);
        }

        #region SinUsar

        //// PUT: api/Incidencias/5
        ////[HttpPut("{id}")]
        //public async Task<IActionResult> PutIncidencia([FromRoute] string id, [FromBody] Incidencia incidencia)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != incidencia.TipoIncidencia)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(incidencia).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }

        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IncidenciaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //// DELETE: api/Incidencias/5
        ////[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteIncidencia([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var incidencia = await _context.Incidencia.SingleOrDefaultAsync(m => m.TipoIncidencia == id);
        //    if (incidencia == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Incidencia.Remove(incidencia);
        //    await _context.SaveChangesAsync();

        //    return Ok(incidencia);
        //}

        #endregion
    }
}