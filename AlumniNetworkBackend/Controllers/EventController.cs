using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AutoMapper;
using System.Security.Claims;
using AlumniNetworkBackend.Models.DTO.EventDTO;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;

        public EventController(AlumniNetworkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Api endpoint api/Events for GET action which returns all events that 
        /// belongs to Groups and Topics where user is subscribed 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<EventForUserReadDTO>> GetEvents()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userBelongsToGroup = await _context.Groups
                .Where(g => g.Members.Any(u => u.Id == userId))
                .ToListAsync();

            List<Event> GroupEvents = userBelongsToGroup
                .SelectMany(e => e.Event)
                .ToList();

            var userBelongsToTopic = await _context.Topics
                .Where(t => t.Users.Any(u => u.Id == userId))
                .ToListAsync();

            List<Event> TopicEvents = userBelongsToTopic
                .SelectMany(e => e.Event)
                .ToList();

            var eventsForGroupsAndTopics = new
            {
                TopicEvents,
                GroupEvents
            };

            return _mapper.Map<EventForUserReadDTO>(eventsForGroupsAndTopics);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }
        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("{event_id}/invite/group/{group_id}")]
        //public async Task<ActionResult<EventCreateGroupInviteDTO>> PostEvent([FromRoute] int event_id, [FromRoute] int group_id)
        //{
        //    _context.Events.Add(@event);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        //}

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
