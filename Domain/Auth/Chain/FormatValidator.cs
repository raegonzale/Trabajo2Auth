using Domain.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public class FormatValidator : HandlerBase
    {
        private static readonly Regex UserRx = new("^[a-zA-Z0-9._-]{3,20}$");
        public override AuthResult Handle(AuthRequest req)
        {
            if (!UserRx.IsMatch(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return new AuthResult { Success = false, Message = "Formato inválido (usuario o password)" };
            return base.Handle(req);
        }
    }
}
