using AlumniNetworkBackend.Controllers;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Helpers
{
    public class JwtMiddleware
    {
        private readonly AlumniNetworkDbContext _dbcontext;
        private readonly IMapper _mapper;

        public JwtMiddleware(AlumniNetworkDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }
        public async Task Invoke(HttpContext context, AlumniNetworkDbContext _dbcontext)
        {
            var token = await context.GetTokenAsync("access_token");
            if (token != null) 
                AttachUserToContext(context,_dbcontext, token);
        }
        public async void AttachUserToContext(HttpContext context, AlumniNetworkDbContext _dbcontext, string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;

                var givenName = tokenS.Claims.First(claim => claim.Type == "name").Value;
                var givenUsername = tokenS.Claims.First(claim => claim.Type == "preferred_username").Value;

                var dto = new UserCreateDTO()
                {
                    Name = givenName,
                    Username = givenUsername
                };

                User user = _mapper.Map<User>(dto);
                _dbcontext.Users.Add(user);
                await _dbcontext.SaveChangesAsync();
                //return CreatedAtRouteResult("GetUser", new { id = user.Id, name = user.Name, username = user.Username }, _mapper.Map<UserReadDTO>(dto));
            }
            catch
            {
            // do nothing if validation fails 
            }
        }
    }
}
