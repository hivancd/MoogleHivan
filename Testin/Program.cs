using MoogleEngine;

bool CheckSort()
{
    SearchItem[] Example =
    {
    new SearchItem("Ultimo", "Lorem ipsum dolor sit amet", 0),
    new SearchItem("Primero", "Lorem ipsum dolor sit amet", 9),
    new SearchItem("Segundo", "Lorem ipsum dolor sit amet", 8),
    };


    var ans = Moogle.DescendingSort(Example);
    foreach (SearchItem SIU in ans)
        System.Console.WriteLine(SIU.Title);
    if (ans[0].Title != "Primero")
        return false;

    SearchItem[] Example1 =
       {
    new SearchItem("Ultimo", "Lorem ipsum dolor sit amet", 9),
    new SearchItem("Primero", "Lorem ipsum dolor sit amet", 9),
    new SearchItem("Segundo", "Lorem ipsum dolor sit amet", 8),
    };


    ans = Moogle.DescendingSort(Example1);
    foreach (SearchItem SIU in ans)
        System.Console.WriteLine(SIU.Title);
    if (ans[0].Score != 9)
        return false;

    return true;
}

if (!CheckSort())
{
    System.Console.WriteLine("EL ORDENAMIENTO DE LOS ARRAY ESTA ROTO");
}





// System.Console.WriteLine("JDFKF");
// int[] DescendingSort(int[] array)//Solo funciona con arrays de  numeros positivos
// {
//     int[] newArray = new int[array.Length];

//     for(int i = 0; i<array.Length; i++)
//     {
//         int a = Max(array);
//         int Index = Array.IndexOf(array, a);
//         array[Index] = -1;
//         newArray[i] = a;
//     }
//     return newArray;
// }
// int Max(int[] array)
// {
//     int Max = array[0];
//     for (int i = 0; i < array.Length; i++)
//     {
//         if (array[i] > Max)
//             Max = array[i];
//     }
//     return Max;
// }


// foreach(int i in DescendingSort(array))
// {
//     System.Console.WriteLine(i);
// }








// System.Console.WriteLine("abcasdfwefabc".IndexOf("abcdsd"));



