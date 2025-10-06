using Domain.Iteration.Implementations;
using Domain.Iteration.Interfaces;
using Domain.Org.Interfaces;
using Legacy;


namespace Domain.Org.Entities;


public class Usuario : IOrgNode
{
    public User LegacyUser { get; }
    public IOrgNode? Parent { get; private set; }
    public Usuario(User legacy, IOrgNode? parent = null) { LegacyUser = legacy; Parent = parent; }
    public string GetUsername() => LegacyUser.getUsername();
    public string GetFirstname() => LegacyUser.getFirstname();
    public string GetName() => $"{LegacyUser.getFirstname()} {LegacyUser.getLastname()}";
    public IReadOnlyList<IOrgNode> GetChildren() => Array.Empty<IOrgNode>();
    public IIterator<IOrgNode> IteratorDFS() => Iterators.Dfs(this);
    public IIterator<IOrgNode> IteratorBFS() => Iterators.Bfs(this);
}
