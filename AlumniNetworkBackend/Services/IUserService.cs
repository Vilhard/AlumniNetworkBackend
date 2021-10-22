using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(int id);
    }
}
