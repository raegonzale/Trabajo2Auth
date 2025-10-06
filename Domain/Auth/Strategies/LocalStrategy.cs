using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Strategies
{
    public class LocalStrategy : IAuthStrategy
    {
        private readonly ICredentialsVerifier _verifier;
        public LocalStrategy(ICredentialsVerifier verifier) => _verifier = verifier;
        public bool Authenticate(string username, string password) => _verifier.Validate(username, password);
    }
}
