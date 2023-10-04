namespace CustomCollections;

internal class Node<T>
{
    public T Value { get; set; }

    public Node<T>? Next { get; set; } = null;

    public Node(T value)
    {
        Value = value;
    }

    public void SetNext(T value)
    {
        var newNode = new Node<T>(value);

        if (Next == null)
        {
            Next = newNode;
        }
        else
        {
            var current = Next;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }
    }

    public int ChildrenCount
    {
        get
        {
            var current = this;
            int count = 0;
            while (current.Next != null)
            {
                current = current.Next;
                count++;
            }

            return count;
        }
    }

    public override string ToString()
    {
        return $"{Value} with {ChildrenCount} children";
    }
}