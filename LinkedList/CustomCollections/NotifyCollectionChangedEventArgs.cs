using System.Collections;

namespace CustomCollections;

public class NotifyCollectionChangedEventArgs<T>
{
    public NotifyCollectionChangedAction Action { get; set; }

    public object? NewItem { get; set; }

    public object? OldItem { get; set; }

    public ICollection<T>? NewItems { get; set; }

    public ICollection<T>? OldItems { get; set; }

    public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
    {
        Action = action;
    }

    public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, T newItem, T oldItem)
        : this(action)
    {
        NewItem = newItem;
        OldItem = oldItem;
    }

    public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, ICollection<T> newItems, ICollection<T> oldItems)
        : this(action)
    {
        NewItems = newItems;
        OldItems = oldItems;
    }

    public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, T newItem, T oldItem, ICollection<T> newItems, ICollection<T> oldItems)
        : this(action, newItem, oldItem)
    {
        NewItems = newItems;
        OldItems = oldItems;
    }
}
