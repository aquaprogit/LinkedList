using System.Collections;

namespace CustomCollections;

public class LinkedList<T> : ICollection<T>, ICloneable
{
    private Node<T>? _root;

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public event Action<NotifyCollectionChangedEventArgs<T>>? CollectionChanged;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException(nameof(index));

            var i = 0;
            var current = _root;
            while (current != null)
            {
                if (i == index)
                {
                    return current.Value;
                }

                i++;
                current = current.Next;
            }

            throw new IndexOutOfRangeException(nameof(index));
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException(nameof(index));

            var i = 0;
            var current = _root;
            while (current != null)
            {
                if (i == index)
                {
                    var previousValue = current.Value;
                    current.Value = value;
                    CollectionChanged?.Invoke(new NotifyCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Update, current.Value, previousValue));
                    return;
                }

                i++;
                current = current.Next;
            }
        }
    }

    public void Add(T item)
    {
        var old = (ICollection<T>)Clone();

        if (_root == null)
            _root = new Node<T>(item);
        else
            _root.SetNext(item);

        Count++;
        CollectionChanged?.Invoke(new NotifyCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, (ICollection<T>)MemberwiseClone(), old));
    }

    public void Clear()
    {
        _root = null;
        Count = 0;
        CollectionChanged?.Invoke(new NotifyCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Clear));
    }

    public bool Contains(T item)
    {
        if (_root == null)
            return false;

        var current = _root;

        while (current != null)
        {
            if (current.Value!.Equals(item))
                return true;

            current = current.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex = 0)
    {
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
            : new LinkedListEnumerator<T>(_root);
    }

    public bool Remove(T item)
    {
        var old = (ICollection<T>)Clone();
        if (_root == null)
            return false;

        if (_root.Value!.Equals(item))
        {
            _root = _root.Next;
            Count--;
            CollectionChanged?.Invoke(new NotifyCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, (ICollection<T>)MemberwiseClone(), old));
            return true;
        }

        var next = _root.Next;
        var current = _root;

        while (next != null)
        {
            if (next.Value!.Equals(item))
            {
                current.Next = next.Next;
                Count--;
                CollectionChanged?.Invoke(new NotifyCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, (ICollection<T>)MemberwiseClone(), old));
                return true;
            }
            current = next;
            next = next.Next;
        }

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object Clone()
    {
        var clone = new LinkedList<T>();
        foreach (var item in this)
        {
            clone.Add(item);
        }
        return clone;
    }
}