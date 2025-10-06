using Domain.Auth.Chain;
using Domain.Auth.DTOs;
using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IHandler _head;
        public AuthService(AuthPipelineBuilder builder) => _head = builder.Build();
        // Compatibilidad con Legacy.User: misma firma
        public bool checkUsernameAndPassword(string username, string password)
        => _head.Handle(new AuthRequest(username, password)).Success;
    }
}
