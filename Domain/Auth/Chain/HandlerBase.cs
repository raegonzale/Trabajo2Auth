using Domain.Auth.DTOs;
using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public abstract class HandlerBase : IHandler
    {
        private IHandler? _next;
        public IHandler SetNext(IHandler next) { _next = next; return next; }
        public virtual AuthResult Handle(AuthRequest req)
        => _next != null ? _next.Handle(req) : new AuthResult { Success = true, Message = "OK" };
    }
}
