using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class LoginController
    {
        private readonly Services.AuthService _auth;
        public LoginController(Services.AuthService auth) => _auth = auth;
        public bool login(string u, string p) => _auth.checkUsernameAndPassword(u, p);
    }

}
