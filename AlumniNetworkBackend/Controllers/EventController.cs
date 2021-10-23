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
using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetEvents()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            List<Event> eventsForGroupAndTopic = _context.Events
                .Where(e=>e.Group.Any(g=>g.Members.Any(m=>m.Id.Contains("2"))) || e.Topic.Any(g => g.Users.Any(m => m.Id.Contains("2"))))
                .ToList();

            return _mapper.Map<List<EventReadDTO>>(eventsForGroupAndTopic);
        }

        // POST: api/Events
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventCreateDTO>> PostNewEvent(EventCreateDTO dtoEvent)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            Event newEvent = new()
            {
                Name = dtoEvent.Name,
                LastUpdated = DateTime.Now,
                Description = dtoEvent.Description,
                AllowGuests = dtoEvent.AllowGuests,
                BannerImg = dtoEvent.BannerImg,
                StartTime = dtoEvent.StartTime,
                EndTime = dtoEvent.EndTime,
                CreatedById = "4",
                TargetGroupId = dtoEvent.TargetGroupId,
                TargetTopicId = dtoEvent.TargetTopicId
            };

            //bool isNotTopicMember = _context.Topics
            //    .Where(t => t.Id == newEvent.TargetTopicId)
            //    .Where(t => t.Users.Any(u => u.Id != "4"));

            if(newEvent.Group.Where(g => g.Id == newEvent.TargetGroupId).Any(m => m.Members.Any(m=>m.Id != "4")) && newEvent.Topic.Where(g => g.Id == newEvent.TargetTopicId).Any(m => m.Users.Any(u=>u.Id != "4")))
            {
                return new StatusCodeResult(403);
            }

            //if (_context.Events.Where(e => e.Group.Any(g => g.Members.Any(m => m.Id.Contains("4"))).Equals(true) || e.Topic.Any(g => g.Users.Any(m => m.Id.Contains("4")))).Equals(true))
            //{
            Event domainEvent = _mapper.Map<Event>(dtoEvent);
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvents", new { id = domainEvent.Id }, dtoEvent);
            //}

            //return new StatusCodeResult(403);
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

        //// POST: api/Events
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Event>> PostEvent(Event @event)
        //{
        //    _context.Events.Add(@event);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        //}
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
