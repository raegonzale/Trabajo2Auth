using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Interfaces
{
    public interface ICredentialsVerifier
    {
        bool Validate(string username, string password);
    }
}
