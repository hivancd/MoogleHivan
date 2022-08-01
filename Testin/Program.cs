﻿using MoogleEngine;
using System.IO;

// int FilesOccur




double tf (int raw_count,string text)
{
    string[] text_array = text.Split();
    return ((double)raw_count/(double)(text_array.Length));
}

double idf(int docs_in_corpus,int docs_with_occur)
{
    if(docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
        return 0;
    return Math.Log(10,(double)docs_in_corpus/(double)docs_with_occur);
}

// System.Console.WriteLine(TF(4,"pldsk paek ldkoekfod sdkmfol sdlkfm skmsokd sdsfs e rsf fsvgs asaf  sfsf sdsa sdsf "));
// System.Console.WriteLine(idf(100,100));









// string stop_words_archive = @"E:\Prog\00moogle\moogle-main\stopwords.txt";
// string[] stopwords = File.ReadAllLines(stop_words_archive);

// string erase_whitespace(string sentence)
// {
//     string ans = "";

//     for (int i = 0; i < sentence.Length; i++)
//     {
//         if(!( sentence[i].ToString() == " " &&  ((i+1 == sentence.Length ) || (sentence[i+1].ToString() == " "))))
//             ans = ans + sentence[i] ;
//     }
//     return ans;
// }

// void Test_whitespace()
// {
//     System.Console.WriteLine(erase_whitespace("can                                   can   df   e"));
// }

// Test_whitespace();
// string delete_stopwords(string sentence)
// {
//     string[] array_sentence = sentence.Split();
//     string ans = "";

//     for (int i = 0; i < array_sentence.Length; i++)
//     {
//         if (!Is_stop_word(array_sentence[i]))
//             ans = ans + array_sentence[i] + " ";
//     }
//     return ans;
// }

// bool Is_stop_word(string word)
// {
//     string stop_words_archive = @"E:\Prog\00moogle\moogle-main\stopwords.txt";
//     string[] stopwords = File.ReadAllLines(stop_words_archive);

//     if (stopwords.Contains(word.ToLower()))
//         return true;
//     return false;
// }

// void Test_stopword()
// {
//     // foreach( string line in stopwords)
//     //     System.Console.Write(line + " ");
//     System.Console.WriteLine(delete_stopwords("a las    d "));
// }

// Test_stopword();

// void WriteSearchItem(SearchItem item)
// {
//     System.Console.WriteLine("Titulo:" + item.Title);
//     System.Console.WriteLine("Snippet:" + item.Snippet);
//     System.Console.WriteLine("Score:" + item.Score);
// }

// string GetSnip(string text, string word)
// {
//     int middle = text.IndexOf(word);
//     int SnipLeng = 30;

//     if (middle < 15)
//         return text.Substring(0, 30);

//     return text.Substring(middle - 15, 30);
// }

// void TestSnip()
// {
//     var text = @"10) Aliados: el combate de los dioses, de Leandro Calderone Luego de que los seres de luz cumplieron con su misi�n, Venecia, Devi, �mbar, Luz, Inti y Gopal a�n est�n en la tierra. Pero, el se�or de la oscuridad los secuestra para estudiarlos y ver c�mo es la energ�a que desprenden y as� intentar crear una nueva raza de seres humanos exterminando a los corrompidos."
//     ;
//     var word = "combate";

//     System.Console.WriteLine(GetSnip(text,word));
// }

// TestSnip();


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

// int SentenceCountOc(string text, string sentence)
// {
//     string[] words = sentence.Split();
//     int totalOccur = 0;

//     foreach (string word in words)
//     {
//         totalOccur = totalOccur + Moogle.CountOcurrences(text, word);
//         System.Console.WriteLine("palabr: " + word);
//         System.Console.WriteLine("occurences: " + Moogle.CountOcurrences(text, word));
//         System.Console.WriteLine("total: " + totalOccur);
//     }

//     return totalOccur;
// }



// void Tester()
// {
//     string[] sentences =
//     {
// "",
// "de"
//  "el combate de los dioses",
//  "se caracteriza por",
//  "mascota",//5
//  "",//0
//  "adaptabilidad",
//     };
//     string Path = @"E:\Prog\00moogle\moogle-main\Content\Aliados El Combate de los dioses.txt";
//     string Path1 = @"E:\Prog\00moogle\moogle-main\Content\mascotas.txt";

//     foreach (string sentence in sentences)
//     {
//         System.Console.WriteLine("sentence: " + sentence);
//         System.Console.WriteLine(SentenceCountOc(File.ReadAllText(Path), sentence));
//         // System.Console.WriteLine(CountOcurrences(File.ReadAllText(Path), sentence));
//         System.Console.WriteLine(SentenceCountOc(File.ReadAllText(Path1), sentence));

//     }

// }

// Tester();



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



 

// SearchItem item  = new SearchItem("Ultimo", "Lorem ipsum dolor sit amet", 9);
// System.Console.WriteLine(item);