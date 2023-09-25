using CustomCollections;
using Xunit;

namespace CustomCollections.Tests;

public class LinkedListTests
{
    [Fact]
    public void Add_AddsItemToLinkedList()
    {
        // Arrange
        var linkedList = new CustomCollections.LinkedList<int>();

        // Act
        linkedList.Add(42);

        // Assert
        List<int> collection = new List<int>() { 1 };
        Assert.Single(collection);
        Assert.Contains(42, linkedList);
    }

    [Fact]
    public void Clear_RemovesAllItemsFromLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act
        linkedList.Clear();

        // Assert
        Assert.Empty(linkedList);
        Assert.DoesNotContain(1, linkedList);
        Assert.DoesNotContain(2, linkedList);
    }

    [Fact]
    public void Contains_ReturnsTrueIfItemExists()
    {
        // Arrange
        var linkedList = new LinkedList<string>() { "apple", "banana", "cherry" };

        // Act & Assert
        Assert.Contains("banana", linkedList);
        Assert.DoesNotContain("grape", linkedList);
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
        Assert.Equal(new int[] { 1, 2, 3 }, array);
    }

    [Fact]
    public void Indexer_GetReturnsItemAtIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 10, 20, 30 };

        // Act & Assert
        Assert.Equal(10, linkedList[0]);
        Assert.Equal(20, linkedList[1]);
        Assert.Equal(30, linkedList[2]);
    }

    [Fact]
    public void Indexer_SetUpdatesItemAtIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 10, 20, 30 };

        // Act
        linkedList[1] = 25;

        // Assert
        Assert.Equal(25, linkedList[1]);
    }

    [Fact]
    public void Remove_RemovesItemFromLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<string> { "apple", "banana", "cherry" };

        // Act
        var result = linkedList.Remove("banana");

        // Assert
        Assert.True(result);
        Assert.Equal(2, linkedList.Count);
        Assert.DoesNotContain("banana", linkedList);
    }

    [Fact]
    public void Remove_ReturnsFalseIfItemNotInLinkedList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(4);

        // Assert
        Assert.False(result);
        Assert.Equal(3, linkedList.Count);
    }

    [Fact]
    public void GetEnumerator_ReturnsEnumerator()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var enumerator = linkedList.GetEnumerator();

        // Assert
        Assert.NotNull(enumerator);
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
        Assert.Equal(new int[] { 1, 2, 3 }, items);
    }

    [Fact]
    public void Indexer_Set_InvalidIndex()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => linkedList[-1] = 5);
        Assert.Throws<IndexOutOfRangeException>(() => linkedList[2] = 5);
    }

    [Fact]
    public void CopyTo_InsufficientSpaceInArray()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        var array = new int[2];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => linkedList.CopyTo(array, 0));
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
        Assert.Equal(1, enumerator.Current);
    }

    [Fact]
    public void Add_MultipleItems()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        linkedList.Add(4);

        // Assert
        Assert.Equal(4, linkedList.Count);
        Assert.Contains(1, linkedList);
        Assert.Contains(2, linkedList);
        Assert.Contains(3, linkedList);
        Assert.Contains(4, linkedList);
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
        enumerator1.MoveNext();
        Assert.Equal(1, enumerator1.Current);

        // Ensure enumerators are independent
        enumerator2.MoveNext();
        Assert.Equal(1, enumerator2.Current);
    }

    [Fact]
    public void Remove_FirstItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(1);

        // Assert
        Assert.True(result);
        Assert.Equal(2, linkedList.Count);
        Assert.DoesNotContain(1, linkedList);
        Assert.Contains(2, linkedList);
    }

    [Fact]
    public void Remove_MiddleItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 3 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        Assert.True(result);
        Assert.Equal(2, linkedList.Count);
        Assert.Contains(1, linkedList);
        Assert.DoesNotContain(2, linkedList);
        Assert.Contains(3, linkedList);
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
        Assert.True(result1);
        Assert.True(result2);
        Assert.Equal(2, linkedList.Count);
        Assert.Contains(1, linkedList);
        Assert.DoesNotContain(2, linkedList);
    }

    [Fact]
    public void GetEnumerator_EmptyList_NoEnumeration()
    {
        // Arrange
        var linkedList = new LinkedList<int>();

        // Act
        var enumerator = linkedList.GetEnumerator();

        // Assert
        Assert.False(enumerator.MoveNext());
    }

    [Fact]
    public void Remove_LastItemInList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        Assert.True(result);
        Assert.Single(linkedList);
        Assert.Contains(1, linkedList);
        Assert.DoesNotContain(2, linkedList);
    }

    [Fact]
    public void Remove_NonExistentItem()
    {
        // Arrange
        var linkedList = new LinkedList<string> { "apple", "banana", "cherry" };

        // Act
        var result = linkedList.Remove("grape");

        // Assert
        Assert.False(result);
        Assert.Equal(3, linkedList.Count);
    }

    [Fact]
    public void Remove_DuplicateItems_RemovedFirst()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1, 2, 2, 3, 2, 4, 2 };

        // Act
        var result = linkedList.Remove(2);

        // Assert
        Assert.True(result);
        Assert.Equal(6, linkedList.Count);
        Assert.Equal(3, linkedList.Count(i => i == 2));
    }

    [Fact]
    public void Remove_LastItemInList_EmptyList()
    {
        // Arrange
        var linkedList = new LinkedList<int> { 1 };

        // Act
        var result = linkedList.Remove(1);

        // Assert
        Assert.True(result);
        Assert.Empty(linkedList);
        Assert.DoesNotContain(1, linkedList);
    }
}
