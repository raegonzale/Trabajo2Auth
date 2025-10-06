using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Interfaces
{
    public interface IAuditSink
    {
        void Write(string message);
        IEnumerable<string> Entries { get; }
    }
}
