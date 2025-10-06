using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Strategies
{
    public class LdapStrategy : IAuthStrategy
    {
        public bool Authenticate(string username, string password)
        {
            // TODO: Integración real LDAP
            return false;
        }
    }
}
