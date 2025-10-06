using System.Collections.Generic;
using Domain.Iteration.Implementations;
using Domain.Iteration.Interfaces;
using Domain.Org.Interfaces;


namespace Domain.Org.Entities;


public class Sede : IOrgNode
{
    private readonly List<IOrgNode> _children = new();
    public string Nombre { get; }
    public Sede(string nombre) => Nombre = nombre;
    public void AddChild(IOrgNode n) => _children.Add(n);
    public void RemoveChild(IOrgNode n) => _children.Remove(n);
    public string GetName() => Nombre;
    public IReadOnlyList<IOrgNode> GetChildren() => _children;
    public IIterator<IOrgNode> IteratorDFS() => Iterators.Dfs(this);
    public IIterator<IOrgNode> IteratorBFS() => Iterators.Bfs(this);
}