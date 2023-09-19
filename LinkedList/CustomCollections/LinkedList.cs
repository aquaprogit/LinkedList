using System.Collections;

namespace CustomCollections;

public class LinkedList<T> : ICollection<T>
{
    private Node<T>? _root;

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        if (_root == null)
            _root = new Node<T>(item);
        else
            _root.SetNext(item);

        Count++;
    }

    public void Clear()
    {
        _root = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        if (_root == null)
            return false;

        var current = _root;

        while (current.Next != null)
        {
            if (current.Value!.Equals(item))
                return true;

            current = current.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex = 0)
    {
        if (_root == null)
            throw new InvalidOperationException("Collection is empty.");

        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < Count)
            throw new ArgumentException("Not enough elements after arrayIndex in the destination array.");

        int index = 0;
        var current = _root;

        while (current != null)
        {
            array[index + arrayIndex] = current.Value;

            index++;
            current = current.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _root == null
            ? Enumerable.Empty<T>().GetEnumerator()
            : new LinkedListEnumerator<T>(ref _root);
    }

    public bool Remove(T item)
    {
        if (_root == null)
            return false;

        if (_root.Next == null)
        {
            if (_root.Value!.Equals(item))
            {
                _root = null;
                Count--;
                return true;
            }

            return false;
        }
        var next = _root.Next;
        var current = _root;

        while (next.Next != null)
        {
            if (next.Value!.Equals(item))
            {
                current.Next.SetNext(next.Next);
                Count--;
                return true;
            }
            next = next.Next;
        }

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class LinkedListEnumerator<T> : IEnumerator<T>
{
    private Node<T> _currenctNode;
    private readonly Node<T> _root;

    public LinkedListEnumerator(ref Node<T> node)
    {
        _root = node;
        _currenctNode = node;
    }

    public T Current => _currenctNode.Value;

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_currenctNode.Next == null)
            return false;

        _currenctNode = _currenctNode.Next;
        return true;
    }

    public void Reset()
    {
        _currenctNode = _root;
    }

    void IDisposable.Dispose() { }
}
