using Domain.Iteration.Interfaces;
using Domain.Org.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Iteration.Implementations
{
    public class DFSIterator : IIterator<IOrgNode>
    {
        private readonly Stack<IOrgNode> _stack = new();
        public DFSIterator(IOrgNode root) => _stack.Push(root);
        public bool HasNext() => _stack.Count > 0;
        public IOrgNode Next()
        {
            var node = _stack.Pop();
            var children = node.GetChildren();
            for (int i = children.Count - 1; i >= 0; i--) _stack.Push(children[i]);
            return node;
        }
    }
}
