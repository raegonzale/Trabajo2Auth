using Domain.Auth.DTOs;
using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public class AuditTrail : HandlerBase
    {
        private readonly IAuditSink _sink;
        public AuditTrail(IAuditSink sink) => _sink = sink;
        public override AuthResult Handle(AuthRequest req)
        {
            _sink.Write($"Auth intent for '{req.Username}'");
            var res = base.Handle(req);
            _sink.Write($"Auth result for '{req.Username}': {(res.Success ? "OK" : "FAIL")} - {res.Message}");
            return res;
        }
    }
}
