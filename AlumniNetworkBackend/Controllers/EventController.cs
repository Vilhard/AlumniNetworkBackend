using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.EventDTO;
using AlumniNetworkBackend.Models.DTO.RSVPDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// Api endpoint api/event for GET action which returns all events that 
        /// belongs to Groups and Topics where user is subscribed 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetEvents()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            List<Event> eventsForGroupAndTopic = await _context.Events
                .Where(e=>e.Group.Any(g=>g.Members.Any(m=>m.Id.Contains(userId))) || e.Topic.Any(g => g.Users.Any(m => m.Id.Contains(userId))))
                .ToListAsync();

            return _mapper.Map<List<EventReadDTO>>(eventsForGroupAndTopic);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> GetSingleEvent(int id)
        {
            Event singleEvent = await _context.Events.FindAsync(id);
            if (singleEvent == null)
                return NotFound(null);

            return Ok(_mapper.Map<EventReadDTO>(singleEvent));
        }
        /// <summary>
        /// EndPoint api/event for Post action which specifies target audience and post if user
        /// is a member. 
        /// </summary>
        /// <param name="dtoEvent"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                CreatedById = userId,
                TargetGroupId = dtoEvent.TargetGroupId,
                TargetTopicId = dtoEvent.TargetTopicId
            };

            bool isNotTopicMember = _context.Topics
                .Where(t => t.Id == dtoEvent.TargetTopicId)
                .Where(t => t.Users.Any(u => u.Id == userId))
                .Equals(false);

            bool isNotGroupMember = _context.Groups
                .Where(g => g.Id == dtoEvent.TargetGroupId)
                .Where(g => g.Members.Any(m => m.Id == userId))
                .Equals(false);

            if ( isNotGroupMember == true && isNotTopicMember == true)
            {
                return new StatusCodeResult(403);
            }

            Event domainEvent = _mapper.Map<Event>(dtoEvent);
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvents", new { id = domainEvent.Id }, dtoEvent);
        }

        /// <summary>
        /// Endpoint api/event/id Updates event table if user is the creator of the event.
        /// Lastupdated is automatically updated by service.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoEvent"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> PutPost(int id, EventUpdateDTO dtoEvent)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = _context.Events.Where(g => g.Id == id).Single();

            if (id != dtoEvent.Id)
            {
                return BadRequest();
            }
            if (thisEvent.CreatedById != userId)
            {
                return new StatusCodeResult(403);
            }
            if (!EventExists(id))
            {
                return NotFound();
            }

            thisEvent.LastUpdated = DateTime.Now;
            if (dtoEvent.Name != null)
                thisEvent.Name = dtoEvent.Name;
            if (dtoEvent.Description != null)
                thisEvent.Description = dtoEvent.Description;
            if (dtoEvent.AllowGuests != null)
                thisEvent.AllowGuests = dtoEvent.AllowGuests;
            if (dtoEvent.BannerImg != null)
                thisEvent.BannerImg = dtoEvent.BannerImg;
            if (dtoEvent.StartTime != null)
                thisEvent.StartTime = dtoEvent.StartTime;
            if (dtoEvent.EndTime != null)
                thisEvent.EndTime = dtoEvent.EndTime;

            await _context.SaveChangesAsync();

            return _mapper.Map<EventReadDTO>(thisEvent);
        }

        /// <summary>
        /// Api endpoint where user can create a new invitation to event by providing
        /// eventid and id of a group user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpPost("{eventId}/invite/group/{groupId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> PostEventGroupInvite([FromRoute] int eventId, int groupId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e=>e.Group).Where(g => g.Id == eventId).FirstAsync();
            var thisGroup = await _context.Groups.Where(m => m.Id == groupId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisGroup == null)
                return NotFound();

            List<Group> groups = thisEvent.Group.ToList();
            groups.Add(thisGroup);

            thisEvent.Group = groups;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can delete an invitation to event by providing
        /// eventid and id of a group user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}/invite/group/{groupId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> DeleteEventGroupInvite([FromRoute] int eventId, int groupId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Group).Where(g => g.Id == eventId).FirstAsync();
            var thisGroup = await _context.Groups.Where(m => m.Id == groupId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisGroup == null)
                return NotFound();

            List<Group> groups = thisEvent.Group.ToList();
            groups.Remove(thisGroup);

            thisEvent.Group = groups;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can create a new invitation to event by providing
        /// eventid and id of a topic user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpPost("{eventId}/invite/topic/{topicId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> PostEventTopicInvite([FromRoute] int eventId, int topicId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Topic).Where(g => g.Id == eventId).FirstAsync();
            var thisTopic = await _context.Topics.Where(m => m.Id == topicId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisTopic == null)
                return NotFound();

            List<Topic> topics = thisEvent.Topic.ToList();
            topics.Add(thisTopic);

            thisEvent.Topic = topics;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can delete an invitation to event by providing
        /// eventid and id of a topic user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}/invite/topic/{topicId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> DeleteEventTopicInvite([FromRoute] int eventId, int topicId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Topic).Where(g => g.Id == eventId).FirstAsync();
            var thisTopic = await _context.Topics.Where(m => m.Id == topicId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisTopic == null)
                return NotFound();

            List<Topic> topics = thisEvent.Topic.ToList();
            topics.Remove(thisTopic);

            thisEvent.Topic = topics;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can create a new invitation to event by providing
        /// eventid and id of a user user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="usersId"></param>
        /// <returns></returns>
        [HttpPost("{eventId}/invite/user/{usersId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> PostEventUserInvite([FromRoute] int eventId, string usersId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Users).Where(g => g.Id == eventId).FirstAsync();
            var thisUser = await _context.Users.Where(m => m.Id == usersId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisUser == null)
                return NotFound();

            List<User> users = thisEvent.Users.ToList();
            users.Add(thisUser);

            thisEvent.Users = users;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can delete an invitation to event by providing
        /// eventid and id of a user user wants to invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="usersId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}/invite/user/{usersId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EventReadDTO>> DeleteEventUserInvite([FromRoute] int eventId, string usersId)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Users).Where(g => g.Id == eventId).FirstAsync();
            var thisUser = await _context.Users.Where(m => m.Id == usersId).FirstAsync();

            if (thisEvent.CreatedById != userId)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisUser == null)
                return NotFound();

            List<User> users = thisEvent.Users.ToList();
            users.Remove(thisUser);

            thisEvent.Users = users;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventReadDTO>(thisEvent));
        }

        /// <summary>
        /// Api endpoint where user can create a new RSVP to event by providing
        /// event id
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dtoRsvp"></param>
        /// <returns></returns>
        [HttpPost("{eventId}/RSVP")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RsvpDTO>> PostEventRSVP([FromRoute] int eventId, RsvpDTO dtoRsvp)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var thisEvent = await _context.Events.Include(e => e.Users).Include(e=>e.Group).Include(e=>e.Topic).Where(g => g.Id == eventId).FirstAsync();
            var thisUser = await _context.Users.Where(u => u.Id == userId).FirstAsync();
            int thisRSVPQuestCount = _context.RSVP.Where(m => m.EventId == eventId).Count()+1;
            var eventGroupMember = thisEvent.Group.Where(g => g.Members.Any(m => m.Id == userId));
            var eventTopicMember = thisEvent.Topic.Where(g => g.Users.Any(m => m.Id == userId));

            if (eventGroupMember == null || eventGroupMember == null)
                return new StatusCodeResult(403);
            if (!EventExists(eventId))
                return NotFound();
            if (thisEvent== null)
                return NotFound();

            RSVP newRSVPRecord = new()
            {
                LastUpdated = DateTime.Now,
                GuestCount = thisRSVPQuestCount,
                UserId = dtoRsvp.UserId,
                EventId = eventId,
            };

            RSVP domainRSVP = _mapper.Map<RSVP>(dtoRsvp);
            _context.RSVP.Add(newRSVPRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = domainRSVP.Id }, dtoRsvp);
        }
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
