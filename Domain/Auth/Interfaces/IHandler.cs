using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Interfaces
{
    public interface IHandler
    {
        IHandler SetNext(IHandler next);
        DTOs.AuthResult Handle(DTOs.AuthRequest req);
    }
}
