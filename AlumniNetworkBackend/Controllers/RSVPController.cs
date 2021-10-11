using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSVPController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;

        public RSVPController(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        // GET: api/RSVPs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RSVP>>> GetRSVP()
        {
            return await _context.RSVP.ToListAsync();
        }

        // GET: api/RSVPs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RSVP>> GetRSVP(int id)
        {
            var rSVP = await _context.RSVP.FindAsync(id);

            if (rSVP == null)
            {
                return NotFound();
            }

            return rSVP;
        }

        // PUT: api/RSVPs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRSVP(int id, RSVP rSVP)
        {
            if (id != rSVP.Id)
            {
                return BadRequest();
            }

            _context.Entry(rSVP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RSVPExists(id))
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

        // POST: api/RSVPs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RSVP>> PostRSVP(RSVP rSVP)
        {
            _context.RSVP.Add(rSVP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRSVP", new { id = rSVP.Id }, rSVP);
        }

        // DELETE: api/RSVPs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRSVP(int id)
        {
            var rSVP = await _context.RSVP.FindAsync(id);
            if (rSVP == null)
            {
                return NotFound();
            }

            _context.RSVP.Remove(rSVP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RSVPExists(int id)
        {
            return _context.RSVP.Any(e => e.Id == id);
        }
    }
}
