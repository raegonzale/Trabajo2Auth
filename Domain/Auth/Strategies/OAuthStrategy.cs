using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Strategies
{
    public class OAuthStrategy : IAuthStrategy
    {
        public bool Authenticate(string username, string password)
        {
            // TODO: Integración real OAuth (PKCE / auth code flow)
            return false;
        }
    }
}
