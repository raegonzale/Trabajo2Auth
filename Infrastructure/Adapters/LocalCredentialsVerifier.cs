using Application.Ports;
using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public class LocalCredentialsVerifier : ICredentialsVerifier
    {
        private readonly IUserRepository _repo;
        public LocalCredentialsVerifier(IUserRepository repo) => _repo = repo;
        public bool Validate(string username, string password)
        {
            var u = _repo.GetByUsername(username);
            return u?.checkUsernameAndPassword(username, password) ?? false;
        }
    }
}
