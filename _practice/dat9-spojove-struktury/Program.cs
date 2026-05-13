using System.Transactions;

LinkedList list = new LinkedList();
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);
list.Add(5);
list.Print();
list.AddFirst(0);
list.Print();

class LinkedList
{
    public Node Head;

    public void Add(int value)
    {
        Node newNode = new Node(value);
        if (Head == null)
        {
            Head = newNode;
            return;
        }
        Node current = Head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = newNode;
    }

    public void AddFirst(int value)
    {
        Node newNode = new Node(value);
        
    }

    public bool Remove (int value)
    {
        Node current = Head;
        while (current.Value != value)
        {
            current = current.Next;
        }
        current.
    }

    public void Print()
    {
        Node current = Head;
        while (current != null)
        {
            Console.Write(current.Value + " → ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    public bool Contains(int value)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Value == value) return true;
            current = current.Next;
        }
        return false;
    }
}

class Node
{
    public int Value;
    public Node Next;
    public Node(int value)
    {
        Value = value;
        Next = null;
    }
}