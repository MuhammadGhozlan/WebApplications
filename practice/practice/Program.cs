
using System.ComponentModel.Design.Serialization;

var l1 = new LinkedList<int>();
l1.AddFirst(9); l1.AddFirst(9); l1.AddFirst(9); l1.AddFirst(9); l1.AddFirst(9); l1.AddFirst(9); l1.AddFirst(9);

var l2 = new LinkedList<int>();
l2.AddFirst(9); l2.AddFirst(9); l2.AddFirst(9); l2.AddFirst(9);




var l4 = sum(l1, l2);
foreach (var item in l4)
{
    Console.WriteLine(item);
}


static LinkedList<int> sum(LinkedList<int> l1, LinkedList<int> l2)
{
    string str1="";
    string str2 = "";
    int count = 0;
    for (int i = 0; i < l1.Count; i++)
    {
        str1 = str1+l1.ElementAt(i).ToString();
    }
    for (int i = 0; i < l2.Count; i++)
    {
        str2 = str2 + l2.ElementAt(i).ToString();
    }
    int NewNumber=Convert.ToInt32(str1) + Convert.ToInt32(str2);
    while (NewNumber > 0)
    {
        NewNumber = NewNumber / 10;
        count++;
    }


    LinkedList<int> l3 = new LinkedList<int>();
    int max = 0;
    int remain = 0;
    max = l1.Count >= l2.Count ? l1.Count : l2.Count;

    ///////////////////////////////////////////
    for (int i = 0 ; i < count; i++)
    {

        var value = 0;
        if (l1.Count==0 && l2.Count!=0)
        {
            value = l2.First() + remain;
            l2.RemoveFirst();
        }
        else if (l2.Count==0 &&l1.Count!=0)
        {
            value = l1.First() + remain;
            l1.RemoveFirst();
        }
        else if (l2.Count==0 && l1.Count==0)
        {
            value =  remain;
        }
        else if(l2.Count!=0 && l1.Count!=0)
        {
            value = l1.First() + l2.First() + remain;
            l1.RemoveFirst(); l2.RemoveFirst();
        }
        



        if (value > 9)
        {

            l3.AddLast(value % 10);
            remain = value / 10;
            
            continue;
        }
        l3.AddLast(value);
        remain = 0;

    }
    return l3;
}

Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("                          New Line                            ");
Console.WriteLine("--------------------------------------------------------------");

 static object BinarySearchIterative(int[] inputArray, int key)
{
    int min = 0;
    int max = inputArray.Length - 1;
    while (min <= max)
    {
        int mid = (min + max) / 2;
        if (key == inputArray[mid])
        {
            return ++mid;
        }
        else if (key < inputArray[mid])
        {
            max = mid - 1;
        }
        else
        {
            min = mid + 1;
        }
    }
    return "Nil";
}
int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
Console.WriteLine(BinarySearchIterative(arr,9) );