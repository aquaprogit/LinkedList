namespace CustomCollections;

internal class Node<T>
{
    public T Value { get; init; }

    public Node<T>? Next { get; private set; } = null;

    public Node(T value)
    {
        Value = value;
    }

    public void SetNext(Node<T> node)
    {
        Next = node;
    }
    public void SetNext(T value)
    {
        var lastNode = this;

        while (lastNode.Next != null)
        {
            lastNode = lastNode.Next;
        }

        lastNode.Next = new Node<T>(value);
    }

    public int ChildrenCount
    {
        get
        {
            var current = this;
            int count = 1;
            while (current != null)
            {
                current = current.Next;
                count++;
            }

            return count;
        }
    }
}