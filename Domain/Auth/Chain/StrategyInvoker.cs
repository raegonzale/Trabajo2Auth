using Domain.Auth.DTOs;
using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public class StrategyInvoker : HandlerBase
    {
        private readonly IAuthStrategy _strategy;
        public StrategyInvoker(IAuthStrategy strategy) => _strategy = strategy;
        public override AuthResult Handle(AuthRequest req)
        {
            var ok = _strategy.Authenticate(req.Username, req.Password);
            return new AuthResult { Success = ok, Message = ok ? "Autenticado" : "Credenciales inválidas" };
        }
    }
}
