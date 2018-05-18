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
    [Route("api/AccesoVehiculos")]
    public class AccesoVehiculosController : Controller
    {
        private readonly GEPAMEContext _context;

        public AccesoVehiculosController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: api/AccesoVehiculos
        [HttpGet]
        public IEnumerable<AccesoVehiculo> GetAccesoVehiculo()
        {
            return _context.AccesoVehiculo;
        }

        // GET: api/AccesoVehiculos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccesoVehiculo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accesoVehiculo = await _context.AccesoVehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);

            if (accesoVehiculo == null)
            {
                return NotFound();
            }

            return Ok(accesoVehiculo);
        }

        // POST: api/AccesoVehiculos
        [HttpPost]
        public async Task<IActionResult> PostAccesoVehiculo([FromBody] AccesoVehiculo accesoVehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccesoVehiculo.Add(accesoVehiculo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccesoVehiculoExists(accesoVehiculo.IdVehiculo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccesoVehiculo", new { id = accesoVehiculo.IdVehiculo }, accesoVehiculo);
        }

        private bool AccesoVehiculoExists(string id)
        {
            return _context.AccesoVehiculo.Any(e => e.IdVehiculo == id);
        }

        //// PUT: api/AccesoVehiculos/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccesoVehiculo([FromRoute] string id, [FromBody] AccesoVehiculo accesoVehiculo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != accesoVehiculo.IdVehiculo)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(accesoVehiculo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccesoVehiculoExists(id))
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

        //// DELETE: api/AccesoVehiculos/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccesoVehiculo([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var accesoVehiculo = await _context.AccesoVehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);
        //    if (accesoVehiculo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AccesoVehiculo.Remove(accesoVehiculo);
        //    await _context.SaveChangesAsync();

        //    return Ok(accesoVehiculo);
        //}
    }
}