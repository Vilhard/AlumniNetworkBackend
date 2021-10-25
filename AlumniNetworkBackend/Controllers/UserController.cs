using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using AlumniNetworkBackend.Models.DTO;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using AlumniNetworkBackend.Services;
using Microsoft.AspNetCore.Cors;
using System.Net.Mime;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public object HttsStatusCode { get; private set; }

        public UserController(AlumniNetworkDbContext context, IMapper mapper, IUserService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserReadDTO>> GetUsers()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var user = await _service.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDTO>(user));
            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            var user = await _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDTO>(user));
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserUpdateDTO>> UpdateUser(string id, UserUpdateDTO dtoUser)
        {
            User user = _mapper.Map<User>(dtoUser);
            var updatedUser = await _service.UpdateUser(id, user);

            if(updatedUser == null)
            {
                return Unauthorized();
            }

            return Ok(_mapper.Map<UserUpdateDTO>(updatedUser));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserCreateDTO>> PostUser(UserCreateDTO newUser)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            if (userId != newUser.Id)
                return Unauthorized();

            User userToAdd = _mapper.Map<User>(newUser);

            var user = await _service.AddUser(userToAdd);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserCreateDTO>(user));
        }
        
    }
}
