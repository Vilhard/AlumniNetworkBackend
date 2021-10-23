using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string id);
        public Task<User> UpdateUser(string id, User dtoUser);
    }
}
