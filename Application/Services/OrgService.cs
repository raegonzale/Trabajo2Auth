using Domain.Iteration.Implementations;
using Domain.Org.Entities;
using Domain.Org.Interfaces;
using Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrgService
    {
        public List<string> listarEmpleados(IOrgNode root)
        {
            var it = Iterators.Bfs(root);
            var list = new List<string>();
            while (it.HasNext())
            {
                var n = it.Next();
                if (n is Usuario u) list.Add($"{u.GetUsername()} - {u.GetName()}");
            }
            return list;
        }


        public List<Usuario> buscarUsuarios(IOrgNode root, BusquedaCriteria c)
        {
            var it = Iterators.Dfs(root);
            var list = new List<Usuario>();
            while (it.HasNext())
            {
                var n = it.Next();
                if (n is Usuario u && c.Matches(u)) list.Add(u);
            }
            return list;
        }
    }
}
