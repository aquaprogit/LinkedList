CustomCollections.LinkedList<int> list = new CustomCollections.LinkedList<int> { 1 };

list.CollectionChanged += List_CollectionChanged;
list[0] = 2;

void List_CollectionChanged(CustomCollections.NotifyCollectionChangedEventArgs<int> obj)
{
    Console.WriteLine(obj);
}

Console.ReadLine();