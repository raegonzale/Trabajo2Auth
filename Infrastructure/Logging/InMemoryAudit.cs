using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class InMemoryAudit : IAuditSink
    {
        private readonly List<string> _entries = new();
        public void Write(string message) => _entries.Add($"{DateTime.UtcNow:o} {message}");
        public IEnumerable<string> Entries => _entries;
    }
}
