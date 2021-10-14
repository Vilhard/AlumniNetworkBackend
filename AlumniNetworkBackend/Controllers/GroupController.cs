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
using AlumniNetworkBackend.Models.DTO.GroupDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;

        public GroupController(AlumniNetworkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Group
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<GroupReadDTO>>> GetGroups()
        {
            //Extra: Used for token testing endpoint
            var token = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var givenName = tokenS.Claims.First(claim => claim.Type == "name").Value;
            var givenUsername = tokenS.Claims.First(claim => claim.Type == "preferred_username").Value;

            List<Group> filteredGroupList = await _context.Groups.Where(g => g.Members
                .Any(user => user.Name == HttpContext.User.Identity.Name))
                .Where(g => g.IsPrivate == false)
                .ToListAsync();

            return _mapper.Map<List<GroupReadDTO>>(filteredGroupList);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDTO>> GetGroup(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            var isNotMember = @group.Members.Where(u => u.Name == HttpContext.User.Identity.Name).Equals(false);
            var isPrivate = @group.IsPrivate.Equals(true);

            if (@group == null)
            {
                return NotFound();
            }
            else if (isNotMember && isPrivate)
            {
                return new StatusCodeResult(403);
            }
            return _mapper.Map<GroupReadDTO>(@group);
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.Id)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupCreateDTO @group)
        {
            var creator = HttpContext.User.Identity;
            Group domainGroup = _mapper.Map<Group>(@group);
            domainGroup.Members.Add((User)creator);
            _context.Groups.Add(domainGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = domainGroup.Id, }, _mapper.Map<GroupReadDTO>(@group));
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
