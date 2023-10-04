using CustomCollections;

CustomCollections.LinkedList<int> ints = new CustomCollections.LinkedList<int>()
{
    1,2,3,4,
};

ints.CollectionChanged += Ints_CollectionChanged;

ints.Add(1);
ints.Remove(4);
ints[2] = 100;
ints.Clear();

void Ints_CollectionChanged(NotifyCollectionChangedEventArgs<int> obj)
{
    string message = obj.Action switch
    {
        NotifyCollectionChangedAction.Add => "Element added",
        NotifyCollectionChangedAction.Remove => "Element removed",
        NotifyCollectionChangedAction.Update => "Collection updated",
        NotifyCollectionChangedAction.Clear => "Collection cleared"
    };
    Console.WriteLine(message);
}

Console.WriteLine();

