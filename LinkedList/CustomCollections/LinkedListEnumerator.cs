using System.Collections;

namespace CustomCollections;

internal class LinkedListEnumerator<T> : IEnumerator<T>
{
    private Node<T>? _currenctNode = null;
    private readonly Node<T> _root;

    public LinkedListEnumerator(Node<T> node)
    {
        _root = node;
    }

    public T Current => _currenctNode.Value;

    object? IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_currenctNode == null)
        {
            _currenctNode = _root;
        }
        else
        {
            if (_currenctNode.Next == null)
                return false;

            _currenctNode = _currenctNode.Next;
        }

        return true;
    }

    public void Reset()
    {
        _currenctNode = null;
    }

    void IDisposable.Dispose() { }
}
