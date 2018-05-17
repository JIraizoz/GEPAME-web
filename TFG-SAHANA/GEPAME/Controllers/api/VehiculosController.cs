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
    [Route("api/Vehiculos")]
    public class VehiculosController : Controller
    {
        private readonly GEPAMEContext _context;

        public VehiculosController(GEPAMEContext context)
        {
            _context = context;
        }

        // GET: api/Vehiculos
        [HttpGet]
        public IEnumerable<Vehiculo> GetVehiculo()
        {
            return _context.Vehiculo;
        }

        // GET: api/Vehiculos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehiculo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }

        // POST: api/Vehiculos
        [HttpPost]
        public async Task<IActionResult> PostVehiculo([FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehiculo.Add(vehiculo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehiculoExists(vehiculo.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehiculo", new { id = vehiculo.Id }, vehiculo);
        }


        private bool VehiculoExists(string id)
        {
            return _context.Vehiculo.Any(e => e.Id == id);
        }

        //// PUT: api/Vehiculos/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVehiculo([FromRoute] string id, [FromBody] Vehiculo vehiculo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != vehiculo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(vehiculo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VehiculoExists(id))
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


        //// DELETE: api/Vehiculos/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVehiculo([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.Id == id);
        //    if (vehiculo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vehiculo.Remove(vehiculo);
        //    await _context.SaveChangesAsync();

        //    return Ok(vehiculo);
        //}
    }
}