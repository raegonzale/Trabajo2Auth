using Domain.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Chain
{
    public class LockoutGuard : HandlerBase
    {
        private readonly Dictionary<string, (int fails, DateTime? lockedUntil)> _state = new(StringComparer.OrdinalIgnoreCase);
        private readonly int _maxAttempts;
        private readonly TimeSpan _lockFor;


        public LockoutGuard(int maxAttempts = 3, TimeSpan? lockFor = null)
        {
            _maxAttempts = maxAttempts;
            _lockFor = lockFor ?? TimeSpan.FromMinutes(5);
        }


        public override AuthResult Handle(AuthRequest req)
        {
            if (_state.TryGetValue(req.Username, out var s) && s.lockedUntil.HasValue && s.lockedUntil.Value > DateTime.UtcNow)
                return new AuthResult { Success = false, Message = "Usuario bloqueado temporalmente" };


            var res = base.Handle(req);


            if (!res.Success)
            {
                var (fails, locked) = _state.TryGetValue(req.Username, out var cur) ? cur : (0, (DateTime?)null);
                fails++;
                if (fails >= _maxAttempts)
                {
                    _state[req.Username] = (fails, DateTime.UtcNow + _lockFor);
                    res.Message = "Bloqueado por intentos fallidos";
                }
                else
                {
                    _state[req.Username] = (fails, null);
                }
            }
            else
            {
                _state[req.Username] = (0, null);
            }
            return res;
        }
    }
}
