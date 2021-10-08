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
    public class EventUserInvitesController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;

        public EventUserInvitesController(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        // GET: api/EventUserInvites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUserInvite>>> GetEventUserInvite()
        {
            return await _context.EventUserInvite.ToListAsync();
        }

        // GET: api/EventUserInvites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventUserInvite>> GetEventUserInvite(int id)
        {
            var eventUserInvite = await _context.EventUserInvite.FindAsync(id);

            if (eventUserInvite == null)
            {
                return NotFound();
            }

            return eventUserInvite;
        }

        // PUT: api/EventUserInvites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventUserInvite(int id, EventUserInvite eventUserInvite)
        {
            if (id != eventUserInvite.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventUserInvite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventUserInviteExists(id))
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

        // POST: api/EventUserInvites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventUserInvite>> PostEventUserInvite(EventUserInvite eventUserInvite)
        {
            _context.EventUserInvite.Add(eventUserInvite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventUserInvite", new { id = eventUserInvite.Id }, eventUserInvite);
        }

        // DELETE: api/EventUserInvites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventUserInvite(int id)
        {
            var eventUserInvite = await _context.EventUserInvite.FindAsync(id);
            if (eventUserInvite == null)
            {
                return NotFound();
            }

            _context.EventUserInvite.Remove(eventUserInvite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventUserInviteExists(int id)
        {
            return _context.EventUserInvite.Any(e => e.Id == id);
        }
    }
}
