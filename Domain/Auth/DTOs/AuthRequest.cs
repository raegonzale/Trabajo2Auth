using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.DTOs
{
    public class AuthRequest
    {
        public string Username { get; }
        public string Password { get; }
        public Dictionary<string, string> Metadata { get; } = new();


        public AuthRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
