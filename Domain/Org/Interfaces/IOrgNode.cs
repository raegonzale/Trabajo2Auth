using Domain.Iteration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Org.Interfaces
{
    public interface IOrgNode
    {
        string GetName();
        IReadOnlyList<IOrgNode> GetChildren();
        IIterator<IOrgNode> IteratorDFS();
        IIterator<IOrgNode> IteratorBFS();
    }
}
