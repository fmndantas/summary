using System.Collections.Generic;
using System.Text;

namespace api.Models.Entities;

public abstract class Container<T> : IEmptable
{
    private List<T> _children;

    public List<T> Children => _children;

    protected Container()
    {
        _children = new List<T>();
    }

    public void AddChild(T child)
    {
        _children.Add(child);
    }

    public int ChildrenCounting => _children.Count;
    
    public abstract bool IsEmpty();

    public abstract string Print(int tab = 0, StringBuilder builder = null);
}