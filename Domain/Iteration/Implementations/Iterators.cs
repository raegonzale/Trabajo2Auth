using Domain.Iteration.Interfaces;
using Domain.Org.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Iteration.Implementations
{
    public static class Iterators
    {
        public static IIterator<IOrgNode> Dfs(IOrgNode root) => new DFSIterator(root);
        public static IIterator<IOrgNode> Bfs(IOrgNode root) => new BFSIterator(root);
    }
}
