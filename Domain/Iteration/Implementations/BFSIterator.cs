using Domain.Iteration.Interfaces;
using Domain.Org.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Iteration.Implementations
{
    public class BFSIterator : IIterator<IOrgNode>
    {
        private readonly Queue<IOrgNode> _q = new();
        public BFSIterator(IOrgNode root) => _q.Enqueue(root);
        public bool HasNext() => _q.Count > 0;
        public IOrgNode Next()
        {
            var node = _q.Dequeue();
            foreach (var c in node.GetChildren()) _q.Enqueue(c);
            return node;
        }
    }
}
