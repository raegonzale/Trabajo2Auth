using Application.Ports;
using Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _byUser = new(StringComparer.OrdinalIgnoreCase);
        public User? GetByUsername(string username) => _byUser.TryGetValue(username, out var u) ? u : null;
        public void Save(User user) => _byUser[user.getUsername()] = user;
    }
}
