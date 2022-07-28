using MoogleEngine;

// int CountOcurrences(string text, string subs)//CountOccur Alternativo Cambia el if
// {
//     if (subs == "")
//         return 0;//No pincha porque el substring es ""

//     int occurrences = 0;
//     int start = text.IndexOf(subs);

//     text = text.ToLower();
//     subs = subs.ToLower();

//     // System.Console.WriteLine(start);

//     if (start != -1 && text[start - 1].ToString() == " " &&  text[start + subs.Length].ToString() == " ") 
//     {
//         System.Console.WriteLine("antes: " + text[start - 1]);
//         System.Console.WriteLine("after: " + text[start + subs.Length]);
//         occurrences++;
//         occurrences = occurrences + CountOcurrences(text.Substring(start + subs.Length), subs);
//     }
//     return occurrences;
// }

int SentenceCountOc(string text, string sentence)
{
    string[] words = sentence.Split();
    int totalOccur = 0;

    foreach (string word in words)
    {
        totalOccur = totalOccur + Moogle.CountOcurrences(text, word);
        System.Console.WriteLine("palabr: " + word);
        System.Console.WriteLine("occurences: " + Moogle.CountOcurrences(text, word));
        System.Console.WriteLine("total: " + totalOccur);
    }

    return totalOccur;
}

// void WriteSearchItem(SearchItem item)
// {
//     System.Console.WriteLine("Titulo:" + item.Title);
//     System.Console.WriteLine("Snippet:" + item.Snippet);
//     System.Console.WriteLine("Score:" + item.Score);
// }

void Tester()
{
    string[] sentences =
    {
        // "",
        // "de"
    //  "el combate de los dioses",
    //  "se caracteriza por",
    //  "mascota",//5
    //  "",//0
    //  "adaptabilidad",
    };
    string Path = @"E:\Prog\00moogle\moogle-main\Content\Aliados El Combate de los dioses.txt";
    string Path1 = @"E:\Prog\00moogle\moogle-main\Content\mascotas.txt";

    foreach (string sentence in sentences)
    {
        System.Console.WriteLine("sentence: " + sentence);
        System.Console.WriteLine(SentenceCountOc(File.ReadAllText(Path), sentence));
        // System.Console.WriteLine(CountOcurrences(File.ReadAllText(Path), sentence));
        System.Console.WriteLine(SentenceCountOc(File.ReadAllText(Path1), sentence));

    }

}

Tester();



// bool CheckSort()
// {
//     SearchItem[] Example =
//     {
//     new SearchItem("Ultimo", "Lorem ipsum dolor sit amet", 0),
//     new SearchItem("Primero", "Lorem ipsum dolor sit amet", 9),
//     new SearchItem("Segundo", "Lorem ipsum dolor sit amet", 8),
//     };


//     var ans = Moogle.DescendingSort(Example);
//     foreach (SearchItem SIU in ans)
//         System.Console.WriteLine(SIU.Title);
//     if (ans[0].Title != "Primero")
//         return false;

//     SearchItem[] Example1 =
//        {
//     new SearchItem("Ultimo", "Lorem ipsum dolor sit amet", 9),
//     new SearchItem("Primero", "Lorem ipsum dolor sit amet", 9),
//     new SearchItem("Segundo", "Lorem ipsum dolor sit amet", 8),
//     };


//     ans = Moogle.DescendingSort(Example1);
//     foreach (SearchItem SIU in ans)
//         System.Console.WriteLine(SIU.Title);
//     if (ans[0].Score != 9)
//         return false;

//     return true;
// }

// if (!CheckSort())
// {
//     System.Console.WriteLine("EL ORDENAMIENTO DE LOS ARRAY ESTA ROTO");
// }





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



