using MoogleEngine;
using System.IO;


// campo

// Sit Rev y su Importancia ...
// animales salvajes
// perros

// los juegos del hambre

// gatos

// Filosofía en Grecia

// gato

// gatos

// mascotas

// perros

// Coincidencias: gatos perros
List<string> NotAllowedWords(string query)
{
    string[] query_array = query.Split();
    List<string> NotAllowedWords = new List<string>();
    if (query == "")
        return NotAllowedWords;
    foreach (string word in query_array)
    {
        if (word[0].ToString() == "!")
            NotAllowedWords.Add(word.Substring(1));
    }
    return NotAllowedWords;
}

string NotAllowedExample = "pepe el !grand";

List<string> NotAllowedTest = NotAllowedWords(NotAllowedExample);

foreach (string word in NotAllowedTest)
    System.Console.WriteLine(word);

// Dictionary<string, int> dict = new Dictionary<string, int>();

// This method returns a dict with each word in the query and the amount of files in wich it appears
// Dictionary<string, int> Query_and_docs_occur(string[] search_query_array, string[] files)
// {
//     Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);s

//     foreach(string word in search_query_array )
//         dict.Add(word, 0);

//     for(int i = 0; i < files.Length; i++)
//     {
//         string CurrentFile = File.ReadAllText(files[i]);

//         foreach(string word in search_query_array)
//         {
//             if (word==""||word==" ")
//                 continue;
//             else if(CurrentFile.Contains(word))
//                 dict[word] += 1;
//         }
//     }    
//     return dict;
// }

// double tf_idf(Dictionary<string,int> Query_docs_occur,string text,int words_in_text,int docs_in_corpus)
// {
//     double tf_idf = 0;

//     foreach(string word in Query_docs_occur.Keys)
//     {
//         tf_idf += Moogle.tf(text,word)*Moogle.idf(docs_in_corpus,Query_docs_occur[word]);
//     }

//     return tf_idf;
// }

// string[] test_array = {@"E:\Prog\00moogle\moogle-main\Content\Aliados El Combate de los dioses.txt",@"E:\Prog\00moogle\moogle-main\Content\Aliados entre el Cielo y la Tierra.txt"};
// string[] test_query_example = {"","dinosaurios"};
// Dictionary<string, int> dict = Query_and_docs_with_occur(test_query_example, test_array);




// double tf (int raw_count,int words_in_text)
// {
//   
//     return ((double)raw_count/(double)words_in_text);
// }

// double idf(int docs_in_corpus,int docs_with_occur)
// {
//     if(docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
//         return 0;
//     return Math.Log(10,(double)docs_in_corpus/(double)docs_with_occur);
// }

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