using MoogleEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

// /*
// What tf-idf needs:
// idf:
// -In how many files the word appears
// -How many files are there
// tf:
// - How many words in text
// - How many times the word appears in the text
// */

// string content = @"E:\Prog\moogle\moogle-main\Content";
// string[] files = Directory.GetFiles(content);

// Dictionary<string, int>[] GetWordDictionarys(string[] files)
// {
//     int FilesLength = files.Length;
//     Dictionary<string, int>[] Dictionarys = new Dictionary<string, int>[FilesLength];

//     for (int i = 0; i < FilesLength; i++)
//     {
//         string[] CurrentFile = File.ReadAllText(files[i]).Split();
//         Dictionary<string, int> CurrentDict = new Dictionary<string, int>();

//         foreach (string word in CurrentFile)
//         {
//             if (CurrentDict.Keys.Contains(word))
//                 CurrentDict[word] += 1;
//             else
//                 CurrentDict[word] = 1;
//         }
//         Dictionarys[i] = CurrentDict;
//     }
//     return Dictionarys;
// }



// void WriteDictInSearchData(Dictionary<string, int> Dict)
// {
//     foreach (string s in Dict.Keys)
//     {
//         System.Console.Write("{" + s + ", " + Dict[s] + "} ");
//     }
//     System.Console.WriteLine();
// }

// var Dictionarys = GetWordDictionarys(files);

// for (int i = 0; i < files.Length; i++)
// {
//     string Filename = Path.GetFileNameWithoutExtension(files[i]);
//     System.Console.WriteLine(Filename);
//     WriteDictInSearchData(Dictionarys[i]);
// }




// foreach (string file in files)
// {
//     string[] CurrentFile = File.ReadAllText(file).Split();
//     foreach (string word in CurrentFile)
//     {
//         if (!AllTheWords.Contains(word))
//             AllTheWords.Add(word);
//     }
// }

// @"E:\Prog\moogle\moogle-main\SearchDictionarys.txt"
// foreach (string word in AllTheWords)
// {
//     File.AppendAllText(@"E:\Prog\moogle\moogle-main\SearchDictionarys.txt", "\n" + word);
// }

// File.WriteAllText(@"E:\Prog\moogle\moogle-main\SearchDictionarys.txt",word);
// File.AppendAllText(@"E:\Prog\moogle\moogle-main\SearchDictionarys.txt", "word");

// Dictionary<string, int> Query_and_docs_occur(string search_query, Dictionary<string, int>[] Dictionarys, string[] files)
// {
//     string[] search_query_array = search_query.Split();
//     Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);

//     foreach (string word in search_query_array)
//         dict.Add(word, 0);

//     for (int i = 0; i < files.Length; i++)
//     {
//         string CurrentFile = files[i];

//         foreach (string word in search_query_array)
//         {
//             if (word == "" || word == " ")
//                 continue;
//             else if (Dictionarys[i].Contains(word))
//                 dict[word] += 1;
//         }
//     }
//     return dict;
// }



