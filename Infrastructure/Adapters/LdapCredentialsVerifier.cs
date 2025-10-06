using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public class LdapCredentialsVerifier : ICredentialsVerifier
    {
        public bool Validate(string username, string password)
        {
            // TODO: Implementar contra LDAP real
            return false;
        }
    }
}
