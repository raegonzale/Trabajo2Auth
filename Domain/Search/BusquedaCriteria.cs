using Domain.Org.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Search
{
    public class BusquedaCriteria
    {
        public string? Username { get; init; }
        public string? Firstname { get; init; }


        public bool Matches(Usuario u)
        {
            bool ok = true;
            if (!string.IsNullOrWhiteSpace(Username))
                ok &= string.Equals(u.GetUsername(), Username, StringComparison.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(Firstname))
                ok &= u.GetFirstname().Contains(Firstname, StringComparison.OrdinalIgnoreCase);
            return ok;
        }
    }
}
