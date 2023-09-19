CustomCollections.LinkedList<int> ints = new CustomCollections.LinkedList<int>();

foreach (var i in Enumerable.Range(1, 10))
{
    ints.Add(i);
}

var arr = new int[15];

ints.CopyTo(arr, 6);

Console.WriteLine("Finished");
Console.ReadLine();