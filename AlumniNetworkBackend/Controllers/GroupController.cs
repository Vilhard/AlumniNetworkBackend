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
using System.Security.Claims;
using System.Net.Mime;
using AlumniNetworkBackend.Services;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GroupController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGroupService _service;

        public GroupController(AlumniNetworkDbContext context, IMapper mapper, IGroupService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }
        /// <summary>
        /// Api endpoint api/Group which returns groups which are either not private,
        /// or the client is member of.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<GroupReadDTO>>> GetGroups()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            List<Group> filteredGroupList = await _service.GetAllGroups(userId);

            return _mapper.Map<List<GroupReadDTO>>(filteredGroupList);
        }

        /// <summary>
        /// Api endpoint api/Group/id which returns group by groupId provided that client is either 
        /// group member or group is not private
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GroupReadDTO>> GetGroupById(int id)
        {
            if (!_service.GroupExists(id))
                return NotFound();

            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            Group filteredGroup = await _service.GetGroupById(id);

            bool isMember = filteredGroup.Members.Any(u => u.Id == userId);
            bool isPrivate = filteredGroup.IsPrivate == true;

            if (isPrivate && !isMember)
            {
                return new StatusCodeResult(403);
            };
            return _mapper.Map<GroupReadDTO>(filteredGroup);
        }

        /// <summary>
        /// Endpoint api/Groups which posts a new Group to database with name
        /// and description.
        /// </summary>
        /// <param name="dtoGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GroupCreateDTO>> PostGroup(GroupCreateDTO dtoGroup)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            Group newGroup = new() { Name = dtoGroup.Name, Description = dtoGroup.Description, IsPrivate = dtoGroup.IsPrivate };
            var updatedGroup = await _service.Create(newGroup, userId);

            if(updatedGroup == null)
            {
                return NotFound(null);
            }

            return Ok(_mapper.Map<GroupCreateDTO>(updatedGroup));
        }

        /// <summary>
        /// Api endpoint api/Group/group_id/join which creates a new group
        /// membership record of the requesting user if group is not private,
        /// members can create membership records regardless
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/join")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GroupCreateMemberDTO>> PostGroupMember([FromRoute] int id)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            Group groupPrivacy = _context.Groups.Include(g=>g.Members).Where(g => g.Id == id).Single();
            Group groupMembers = _context.Groups.Find(id);
            bool isNotAMember = groupMembers.Members.Where(u => u.Id.Contains(userId)).Equals(true);
            var groupNewUserList = await _service.AddUserToGroup(id, userId);


            if (groupPrivacy.IsPrivate == true && isNotAMember)
            {
                return new StatusCodeResult(403);
            }
            if (groupNewUserList == null)
            {
                return NotFound(null);
            }

            return Ok(_mapper.Map<GroupCreateMemberDTO>(groupNewUserList));

        }
    }
}
