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
    [Route("api/Posiciones")]
    public class PosicionesController : Controller
    {
        private readonly GEPAMEContext _context;

        public PosicionesController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: api/Posiciones
        [HttpGet]
        public IEnumerable<Posicion> GetPosicion()
        {
            return _context.Posicion;
        }

        // GET: api/Posiciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosicion([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posicion = await _context.Posicion.SingleOrDefaultAsync(m => m.IdVehiculo == id);

            if (posicion == null)
            {
                return NotFound();
            }

            return Ok(posicion);
        }

        // POST: api/Posiciones
        [HttpPost]
        public async Task<IActionResult> PostPosicion([FromBody] Posicion posicion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Posicion.Add(posicion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PosicionExists(posicion.IdVehiculo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPosicion", new { id = posicion.IdVehiculo }, posicion);
        }

        private bool PosicionExists(string id)
        {
            return _context.Posicion.Any(e => e.IdVehiculo == id);
        }

        //#region SinUsar

        //// PUT: api/Posiciones/5
        ////[HttpPut("{id}")]
        //public async Task<IActionResult> PutPosicion([FromRoute] string id, [FromBody] Posicion posicion)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != posicion.IdVehiculo)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(posicion).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PosicionExists(id))
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



        //// DELETE: api/Posiciones/5
        ////[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePosicion([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var posicion = await _context.Posicion.SingleOrDefaultAsync(m => m.IdVehiculo == id);
        //    if (posicion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Posicion.Remove(posicion);
        //    await _context.SaveChangesAsync();

        //    return Ok(posicion);
        //}

        //#endregion
    }
}