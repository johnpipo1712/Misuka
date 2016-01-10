using System.Collections.Generic;

namespace Misuka.Domain.Utilities
{
  public class SearchResult<T>
  {
    private readonly int _count;
    private readonly IList<T> _items;

    public SearchResult(IList<T> items, int count)
    {
      _items = items;
      _count = count;
    }

    public SearchResult()
    {
      _count = 0;
      _items = new List<T>();
    }

    public IList<T> Items
    {
      get { return _items; }
    }

    public int Count
    {
      get { return _count; }
    }
  }
}