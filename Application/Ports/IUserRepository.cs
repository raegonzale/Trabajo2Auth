using Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        void Save(User user);
    }
}
