using Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public class AuthPipelineBuilder
    {
        private readonly List<IHandler> _steps = new();
        public AuthPipelineBuilder WithAudit(IAuditSink sink) { _steps.Add(new AuditTrail(sink)); return this; }
        public AuthPipelineBuilder WithFormatValidator() { _steps.Add(new FormatValidator()); return this; }
        public AuthPipelineBuilder WithLockoutGuard(int max = 3, TimeSpan? lockFor = null) { _steps.Add(new LockoutGuard(max, lockFor)); return this; }
        public AuthPipelineBuilder WithStrategy(IAuthStrategy s) { _steps.Add(new StrategyInvoker(s)); return this; }
        public IHandler Build()
        {
            if (_steps.Count == 0) throw new InvalidOperationException("No steps configured");
            for (int i = _steps.Count - 1; i > 0; i--) _steps[i - 1].SetNext(_steps[i]);
            return _steps[0];
        }
    }
}
