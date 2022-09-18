using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MoogleEngine;

public static class Moogle
{
    public static SearchResult Query(string query)//Este es el metodo PrincipalMAIN METHOD
    {
        var timer = new Stopwatch();
        timer.Start();

        string content = @"E:\Prog\moogle\moogle-main\Content";
        string[] files = Directory.GetFiles(content);

        string search_query = QueryProcessing.ProcessQuery(query);

        var Dictionarys = WordDictionarys.GetWordDictionarys(files);
        Dictionary<string, int> query_and_docs_occur = Query_and_docs_occur(search_query, Dictionarys);

        SearchItem[] FilesOccur = new SearchItem[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            // Method: Construir el SearchItem
            var text = Dictionarys[i];
            string CurrentFile = File.ReadAllText(files[i]);
            int FileSize = text.Values.Sum();
            string FileName = Path.GetFileNameWithoutExtension(files[i]);
            string snippet = GetSnip(CurrentFile, search_query);
            double Score = tf_idf(query_and_docs_occur, text, FileSize, files.Length, search_query) * (1 + 1 / GetCLoseWords(CurrentFile, query, text));
            System.Console.WriteLine((1 + 1 / GetCLoseWords(CurrentFile, query, text)));
            FilesOccur[i] = new SearchItem(FileName, snippet, (float)Score);
            // tf(count Occur,CurrentFile)
            // idf(files.Length,docsOccur)
        }

        var items = ResultsPreparations.SortSearchItems(FilesOccur);

        foreach (SearchItem item in items)
            System.Console.WriteLine(item);

        timer.Stop();
        System.Console.WriteLine("Time taken: " + timer.Elapsed.ToString(@"m\:ss\.fff"));
        timer.Restart();

        timer.Start();
        var suggestion = "Suggestion.GetSuggestion(files,search_query)";
        // var suggestion = Suggestion.GetSuggestion(files,search_query);
        timer.Stop();
        System.Console.WriteLine("Time taken in suggestion: " + timer.Elapsed.ToString(@"m\:ss\.fff"));
        return new SearchResult(items, suggestion);// Suggestion.GetSuggestion(files,search_query)
    }



    public static double tf_idf(Dictionary<string, int> Query_docs_occur, Dictionary<string, int> text, int words_in_text, int docs_in_corpus, string query)
    {
        double tf_idf = 0;
        List<string> NotAllowedWords = GetNotAllowedWords(query);
        List<string> NecessaryWords = GetNecessaryWords(query);

        foreach (string word in NecessaryWords)
        {
            if (!text.Keys.Contains(word))
                return tf_idf;
        }
        foreach (string word in NotAllowedWords)
        {
            if (text.Keys.Contains(word))
                return tf_idf;
        }
        foreach (string word in Query_docs_occur.Keys)
        {
            tf_idf += Moogle.tf(word, text) * Moogle.idf(docs_in_corpus, Query_docs_occur[word]);
        }

        return tf_idf;
    }
    public static double tf(string subs, Dictionary<string, int> Dictionary)
    {
        int a = 0;
        if (Dictionary.ContainsKey(subs))
            a = Dictionary[subs];
        return ((double)a / (double)Dictionary.Count);
    }


    public static double idf(int docs_in_corpus, int docs_with_occur)
    {
        if (docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
            return 0;
        return Math.Log(10, (double)docs_in_corpus / (double)docs_with_occur);
    }

    public static List<string> GetNotAllowedWords(string query)
    {
        string[] query_array = query.Split();
        List<string> GetNotAllowedWords = new List<string>();
        if (query == "")
            return GetNotAllowedWords;
        foreach (string word in query_array)
        {
            if (word[0].ToString() == "!")
                GetNotAllowedWords.Add(word.Substring(1));
        }
        return GetNotAllowedWords;
    }
    // static public int GetCLoseWords(string text, string query)
    // {
    //     string pattern = @"[ ~ ]||[~]||[ ~]||[~ ]";
    //     Regex obj = new Regex(pattern);

    //     List<int> distances = new List<int>();
    //     int distance = 0;
    //     int ans = 0;
    //     string[] query_array = query.Split();

    //     for (int i = 0; i < query_array.Length - 2; i++)
    //     {
    //         int LeftWordIndex = text.IndexOf(query_array[i]);
    //         int RightWordIndex = text.IndexOf(query_array[i + 2]);

    //         if (obj.IsMatch(query_array[i + 1]) && LeftWordIndex >= 0 && RightWordIndex >= 0)
    //         {
    //             distance = (Math.Abs(LeftWordIndex - RightWordIndex));
    //             distances.Add(distance);
    //         }
    //     }
    //     foreach (int e in distances)
    //         ans += e;

    //     return ans;
    // }
    public static string GetSnip(string text, string query)//Este metodo coge el snip (Es mejorable)ARREGLAR
    {
        string[] query_array = query.Split();
        int middle = -1;
        int SnipLeng = 30;

        foreach (string word in query_array)
        {
            middle = text.IndexOf(word);
            if (middle >= 0)
            {
                if (text.Length <= SnipLeng)
                    return text;
                if (text.Length - middle <= SnipLeng)
                    return text.Substring(middle, text.Length - middle);

                if (middle < 15)
                    return text.Substring(0, SnipLeng);
                else
                    return text.Substring(middle - 15, SnipLeng);
            }
        }
        return " ";
    }


    public static double GetCLoseWords(string text, string query, Dictionary<string, int> Dict)
    {
        string pattern = @"[ ~ ]||[~]||[ ~]||[~ ]";
        Regex obj = new Regex(pattern);

        string[] query_array = query.Split();

        if (query_array.Length >= 3)
        {
            for (int i = 0; i < query_array.Length - 2; i++)
            {
                string LeftWord = query_array[i];
                string RightWord = query_array[i + 2];

                if (obj.IsMatch(query_array[i + 1]) && Dict.ContainsKey(LeftWord) && Dict.ContainsKey(RightWord))
                {
                    List<int> distances = new List<int>();
                    var LeftWordIndexes = GetIndexes(text, query_array[i], Dict[query_array[i]]);
                    var RightWordIndexes = GetIndexes(text, query_array[i + 2], Dict[query_array[i + 2]]);

                    foreach (int indexL in LeftWordIndexes)
                    {
                        foreach (int indexR in RightWordIndexes)
                        {
                            int distance = (Math.Abs(indexL - indexR));
                            distances.Add(distance);
                        }
                    }
                    int ans = distances[0];
                    foreach (int dist in distances)
                    {
                        if (dist < ans)
                            ans = dist;
                    }
                    return (double)ans;
                }
            }
        }
        return 1;
    }

    public static int[] GetIndexes(string text, string word, int cant)
    {
        int[] AllIndexes = new int[cant];
        int wordIndex = text.IndexOf(word);
        int Mark = 0;

        for (int i = 0; i < cant; i++)
        {
            AllIndexes[i] = text.Substring(Mark + word.Length).IndexOf(word);
            Mark = AllIndexes[i];
        }
        return AllIndexes;
    }


    // This method returns a dict with each word in the query and the amount of files in wich it appears
    // public static Dictionary<string, int> Query_and_docs_occur(string search_query, string[] files)
    // {
    //     string[] search_query_array = search_query.Split();
    //     Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);

    //     foreach (string word in search_query_array)
    //         dict.Add(word, 0);

    //     for (int i = 0; i < files.Length; i++)
    //     {
    //         string CurrentFile = File.ReadAllText(files[i]);

    //         foreach (string word in search_query_array)
    //         {
    //             if (word == "" || word == " ")
    //                 continue;
    //             else if (CurrentFile.Contains(word))
    //                 dict[word] += 1;
    //         }
    //     }
    //     return dict;
    // }


    public static Dictionary<string, int> Query_and_docs_occur(string search_query, Dictionary<string, int>[] Dictionarys)
    {
        string[] search_query_array = search_query.Split();
        Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);

        foreach (string word in search_query_array)
            dict.Add(word, 0);

        for (int i = 0; i < Dictionarys.Length; i++)
        {
            foreach (string word in search_query_array)
            {
                if (word == "" || word == " ")
                    continue;
                else if (Dictionarys[i].Keys.Contains(word))
                    dict[word] += 1;
            }
        }
        return dict;
    }
    public static List<string> GetNecessaryWords(string query)
    {
        string[] query_array = query.Split();
        List<string> GetNecessaryWords = new List<string>();
        if (query == "")
            return GetNecessaryWords;
        foreach (string word in query_array)
        {
            if (word[0].ToString() == "^")
                GetNecessaryWords.Add(word.Substring(1));
        }
        return GetNecessaryWords;
    }


    // //Cuenta las ocurrencias de cada palabra de una oracion en un texto y las suma
    // public static int SentenceCountOc(string text, string sentence)
    // {
    //     string[] words = sentence.Split();
    //     int totalOccur = 0;

    //     foreach (string word in words)
    //     {
    //         // TFidf = tf(text, word) * idf(word)
    //         totalOccur += Moogle.RawCount(text, word);
    //     }

    //     return totalOccur;
    // }
    //Ordena Un Array De SearchItems de mayor a menor segun el Score RESULTS PROCESSING


}
// SearchItem[] items =
// {
//     // FilesOccur[0],
//     // FilesOccur[1],
//     // FilesOccur[2]

//     new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
//     new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.5f),
//     new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.1f),
// };

// items = new SearchItem[3];


// //Cuenta las ocurrencias de una palabra en un texto
// public static int RawCount(string text, string subs)
// {
//     if (subs == "")
//         return 0;//No pincha porque el substring es ""
//     int occurrences = 0;
//     int start = text.IndexOf(subs);

//     text = text.ToLower();
//     subs = subs.ToLower();

//     if (start != -1)
//     {
//         occurrences++;
//         occurrences = occurrences + RawCount(text.Substring(start + subs.Length), subs);
//     }
//     return occurrences;
// }

// 0.01905176112949586
// 0.004127538426740262
// 0.0005930665979622655
// 0.0027132793128165906
// 0.00010239393220392612
// 0.0004787893682015874
// 0.001105896176125092
// 0.0013115821798971503
// 0.0026231643597943006