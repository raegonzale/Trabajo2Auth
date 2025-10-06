using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public class OAuthCredentialsVerifier : ICredentialsVerifier
    {
        public bool Validate(string username, string password)
        {
            // TODO: Implementar validación con proveedor OAuth
            return false;
        }
    }
}
