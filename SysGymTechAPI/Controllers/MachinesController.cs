using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;

namespace SysGymTechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly BDContexto _context;

        public MachinesController(BDContexto context)
        {
            _context = context;
        }

        // GET: api/Machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machines>>> GetMachines()
        {
          if (_context.Machines == null)
          {
              return NotFound();
          }
            return await _context.Machines.ToListAsync();
        }

        // GET: api/Machines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Machines>> GetMachines(int id)
        {
          if (_context.Machines == null)
          {
              return NotFound();
          }
            var machines = await _context.Machines.FindAsync(id);

            if (machines == null)
            {
                return NotFound();
            }

            return machines;
        }

        // PUT: api/Machines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachines(int id, Machines machines)
        {
            if (id != machines.Id_Machines)
            {
                return BadRequest();
            }

            _context.Entry(machines).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachinesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Machines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Machines>> PostMachines(Machines machines)
        {
          if (_context.Machines == null)
          {
              return BadRequest("Entity set 'BDContexto.Machines'  is null.");
          }
            _context.Machines.Add(machines);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachines", new { id = machines.Id_Machines }, machines);
        }

        // DELETE: api/Machines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachines(int id)
        {
            if (_context.Machines == null)
            {
                return NotFound();
            }
            var machines = await _context.Machines.FindAsync(id);
            if (machines == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machines);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachinesExists(int id)
        {
            return (_context.Machines?.Any(e => e.Id_Machines == id)).GetValueOrDefault();
        }
    }
}
