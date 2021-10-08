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
    public class EventGroupInvitesController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;

        public EventGroupInvitesController(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        // GET: api/EventGroupInvites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventGroupInvite>>> GetEventGroupInvite()
        {
            return await _context.EventGroupInvite.ToListAsync();
        }

        // GET: api/EventGroupInvites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventGroupInvite>> GetEventGroupInvite(int id)
        {
            var eventGroupInvite = await _context.EventGroupInvite.FindAsync(id);

            if (eventGroupInvite == null)
            {
                return NotFound();
            }

            return eventGroupInvite;
        }

        // PUT: api/EventGroupInvites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventGroupInvite(int id, EventGroupInvite eventGroupInvite)
        {
            if (id != eventGroupInvite.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventGroupInvite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventGroupInviteExists(id))
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

        // POST: api/EventGroupInvites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventGroupInvite>> PostEventGroupInvite(EventGroupInvite eventGroupInvite)
        {
            _context.EventGroupInvite.Add(eventGroupInvite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventGroupInvite", new { id = eventGroupInvite.Id }, eventGroupInvite);
        }

        // DELETE: api/EventGroupInvites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventGroupInvite(int id)
        {
            var eventGroupInvite = await _context.EventGroupInvite.FindAsync(id);
            if (eventGroupInvite == null)
            {
                return NotFound();
            }

            _context.EventGroupInvite.Remove(eventGroupInvite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventGroupInviteExists(int id)
        {
            return _context.EventGroupInvite.Any(e => e.Id == id);
        }
    }
}
