using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy
{
    public class User
    {
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;


        public string getFirstname() => firstname;
        public string getLastname() => lastname;
        public string getUsername() => username;


        public bool checkUsernameAndPassword(string username, string password)
        => this.username == username && this.password == password;


        public void setFirstname(string v) => firstname = v;
        public void setLastname(string v) => lastname = v;
        public void setUsername(string v) => username = v;
        public void setPassword(string v) => password = v;
    }
}
