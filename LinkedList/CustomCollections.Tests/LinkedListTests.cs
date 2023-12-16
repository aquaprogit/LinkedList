using FluentAssertions;

namespace CustomCollections.Tests;

public class LinkedListTests
{
    [Fact]
    public void Add_AddsItemToLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int>();

        // Act
        linkedList.Add(42);

        // Assert
        linkedList.Should().ContainSingle().Which.Should().Be(42);
    }

    [Fact]
    public void Clear_RemovesAllItemsFromLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act
        linkedList.Clear();

        // Assert
        linkedList.Should().BeEmpty();
    }

    [Fact]
    public void Contains_ReturnsTrueIfItemExists()
    {
        // Arrange
        var linkedList = new LinkedList<string>() { "apple", "banana", "cherry" };

        var result = linkedList.Contains("banana");

        // Act & Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CopyTo_CopiesItemsToArray()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };
        var array = new int[3];

        // Act
        linkedList.CopyTo(array, 0);

        // Assert
        array.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Indexer_GetReturnsItemAtIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 10, 20, 30 };

        // Act & Assert
        linkedList[0].Should().Be(10);
        linkedList[1].Should().Be(20);
        linkedList[2].Should().Be(30);
    }

    [Fact]
    public void Indexer_SetUpdatesItemAtIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 10, 20, 30 };

        // Act
        linkedList[1] = 25;

        // Assert
        linkedList[1].Should().Be(25);
    }

    [Fact]
    public void Remove_RemovesItemFromLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<string> { "apple", "banana", "cherry" };

        // Act
        var result = linkedList.Remove("banana");

        // Assert
        result.Should().BeTrue();
        linkedList.Should().HaveCount(2).And.NotContain("banana");
    }

    [Fact]
    public void Remove_ReturnsFalseIfItemNotInLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(4);

        // Assert
        result.Should().BeFalse();
        linkedList.Should().HaveCount(3);
    }

    [Fact]
    public void GetEnumerator_ReturnsEnumerator()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var enumerator = linkedList.GetEnumerator();

        // Assert
        enumerator.Should().NotBeNull();
    }

    [Fact]
    public void GetEnumerator_EnumeratesItemsInOrder()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var enumerator = linkedList.GetEnumerator();
        var items = new List<int>();
        while (enumerator.MoveNext())
        {
            items.Add(enumerator.Current);
        }

        // Assert
        items.Should().Equal(1, 2, 3);
    }

    [Fact]
    public void Indexer_Set_InvalidIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act & Assert
        Action action1 = () => linkedList[-1] = 5;
        Action action2 = () => linkedList[2] = 5;

        action1.Should().Throw<IndexOutOfRangeException>();
        action2.Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void CopyTo_InsufficientSpaceInArray()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };
        var array = new int[2];

        // Act & Assert
        Action action = () => linkedList.CopyTo(array, 0);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Enumerator_Reset()
    {
        // Arrange
        var linkedList = new LinkedList<int>() { 1, 2, 3 };
        var enumerator = linkedList.GetEnumerator();

        // Act
        enumerator.MoveNext();
        enumerator.Reset();

        // Assert
        enumerator.MoveNext();
        enumerator.Current.Should().Be(1);
    }

    [Fact]
    public void Add_MultipleItems()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        linkedList.Add(4);

        // Assert
        linkedList.Should().HaveCount(4).And.ContainInOrder(1, 2, 3, 4);
    }

    [Fact]
    public void GetEnumerator_MultipleEnumerations()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var enumerator1 = linkedList.GetEnumerator();
        var enumerator2 = linkedList.GetEnumerator();

        // Assert
        enumerator1.MoveNext().Should().BeTrue();
        enumerator1.Current.Should().Be(1);

        // Ensure enumerators are independent
        enumerator2.MoveNext().Should().BeTrue();
        enumerator2.Current.Should().Be(1);
    }

    [Fact]
    public void Remove_FirstItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(1);

        // Assert
        result.Should().BeTrue();
        linkedList.Should().HaveCount(2).And.NotContain(1).And.ContainInOrder(2, 3);
    }

    [Fact]
    public void Remove_MiddleItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        result.Should().BeTrue();
        linkedList.Should().HaveCount(2).And.ContainInOrder(1, 3).And.NotContain(2);
    }

    [Fact]
    public void Remove_DuplicateItems()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 2, 3 };

        // Act
        var result1 = linkedList.Remove(2);
        var result2 = linkedList.Remove(2);

        // Assert
        result1.Should().BeTrue();
        result2.Should().BeTrue();
        linkedList.Should().HaveCount(2).And.ContainInOrder(1, 3).And.NotContain(2);
    }

    [Fact]
    public void GetEnumerator_EmptyList_NoEnumeration()
    {
        // Arrange
        var linkedList = new LinkedList<int>();

        // Act
        var enumerator = linkedList.GetEnumerator();

        // Assert
        enumerator.MoveNext().Should().BeFalse();
    }

    [Fact]
    public void Remove_LastItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        result.Should().BeTrue();
        linkedList.Should().HaveCount(1).And.ContainInOrder(1).And.NotContain(2);
    }

    [Fact]
    public void Remove_NonExistentItem()
    {
        // Arrange
        var linkedList = new LinkedList<string> { "apple", "banana", "cherry" };

        // Act
        var result = linkedList.Remove("grape");

        // Assert
        result.Should().BeFalse();
        linkedList.Should().HaveCount(3);
    }

    [Fact]
    public void Remove_DuplicateItems_RemovedFirst()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 2, 3, 2, 4, 2 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        result.Should().BeTrue();
        linkedList.Should().HaveCount(6).And.ContainInOrder(1, 2, 3, 2, 4, 2);
    }

    [Fact]
    public void Remove_LastItemInList_EmptyList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1 };

        // Act
        var result = linkedList.Remove(1);

        // Assert
        result.Should().BeTrue();
        linkedList.Should().BeEmpty().And.NotContain(1);
    }

    [Fact]
    public void Clone_NewListElementsTheSame_ClonesLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int>() { 1, 4, -10, 5 };

        // Act
        var result = linkedList.Clone();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(linkedList);
        result.Should().NotBeSameAs(linkedList);
    }

    [Fact]
    public void Clone_EmptyCollection_ClonesEmptyCollection()
    {
        // Arrange
        var linkedList = new LinkedList<int>();

        // Act
        var result = linkedList.Clone();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(linkedList);
        result.Should().NotBeSameAs(linkedList);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(10)]
    public void Index_OutOfBoundsArgumentPassing_ThrowsException(int index)
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 3, 5 };

        // Act
        var result = () => linkedList[index];

        // Assert
        result.Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void CopyTo_NullArray_ThrowsArgumentNullException()
    {
        // Arrange
        var list = new LinkedList<int>();

        // Act
        var act = () => list.CopyTo(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CopyTo_NegativeArrayIndex_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var list = new LinkedList<int>();

        // Act
        var act = () => list.CopyTo(new int[5], -1);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CopyTo_NotEnoughSpaceInArray_ThrowsArgumentException()
    {
        // Arrange
        var list = new LinkedList<int> { 1, 2 };

        // Act
        var act = () => list.CopyTo(new int[1]);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough elements after arrayIndex in the destination array.");
    }

    [Fact]
    public void Remove_EmptyList_ReturnsFalseNothingChangedInList()
    {
        var list = new LinkedList<int>();

        var result = list.Remove(0);

        result.Should().BeFalse();
        list.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void Add_SingleItem_RaisesCollectionChangedEvent()
    {
        // Arrange
        var list = new LinkedList<int>();
        NotifyCollectionChangedEventArgs<int> eventArgs = null!;

        // Act
        list.CollectionChanged += (args) => eventArgs = args;
        list.Add(42);

        // Assert
        eventArgs.Should().NotBeNull();
        eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
        eventArgs.NewItem.Should().BeNull();
        eventArgs.OldItem.Should().BeNull();
        eventArgs.NewItems.Should().BeEquivalentTo(new int[] { 42 });
        eventArgs.OldItems.Should().BeEmpty();
    }

    [Fact]
    public void Remove_Item_RaisesCollectionChangedEvent()
    {
        // Arrange
        var list = new LinkedList<int> { 1, 2, 3 };
        NotifyCollectionChangedEventArgs<int> eventArgs = null;

        // Act
        list.CollectionChanged += (args) => eventArgs = args;
        list.Remove(2);

        // Assert
        eventArgs.Should().NotBeNull();
        eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Remove);
        eventArgs.NewItem.Should().BeNull();
        eventArgs.OldItem.Should().BeNull();
        eventArgs.NewItems.Should().BeEquivalentTo(new int[] { 1, 3 });
        eventArgs.OldItems.Should().BeEquivalentTo(new int[] { 1, 2, 3 });
    }

    [Fact]
    public void Remove_LastItem_RaisesCollectionChangedEventWithRemoveAction()
    {
        // Arrange
        var list = new LinkedList<int> { 42 };
        NotifyCollectionChangedEventArgs<int> eventArgs = null;

        // Act
        list.CollectionChanged += (args) => eventArgs = args;
        list.Remove(42);

        // Assert
        eventArgs.Should().NotBeNull();
        eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Remove);
        eventArgs.NewItem.Should().BeNull();
        eventArgs.OldItem.Should().BeNull();
        eventArgs.NewItems.Should().BeEmpty();
        eventArgs.OldItems.Should().BeEquivalentTo(new int[] { 42 });
    }

    [Fact]
    public void Clear_NotEmptyList_RaisesCollectionChangedEventWithClearAction()
    {
        var list = new LinkedList<int> { 42, 24 };
        NotifyCollectionChangedEventArgs<int> eventArgs = null;

        list.CollectionChanged += (args) => eventArgs = args;
        list.Clear();

        eventArgs.Should().NotBeNull();
        eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Clear);
        eventArgs.NewItem.Should().BeNull();
        eventArgs.OldItem.Should().BeNull();
        eventArgs.NewItems.Should().BeEmpty();
        eventArgs.OldItems.Should().BeEquivalentTo(new int[] { 42, 24 });
    }

    [Theory]
    [InlineData(24, 55)]
    public void Index_SetByIndex_RaisesCollectionChangedEventWithUpdateAction(int oldValue, int value)
    {
        var list = new LinkedList<int> { 42, oldValue };
        NotifyCollectionChangedEventArgs<int> eventArgs = null;

        list.CollectionChanged += (args) => eventArgs = args;
        list[1] = value;

        eventArgs.Should().NotBeNull();
        eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Update);
        eventArgs.NewItem.Should().Be(value);
        eventArgs.OldItem.Should().Be(oldValue);
        eventArgs.NewItems.Should().BeNull();
        eventArgs.OldItems.Should().BeNull();
    }
}
