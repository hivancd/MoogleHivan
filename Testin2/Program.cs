using MoogleEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

Dictionary<string, int>[] GetWordDictionarys(string[] files)
{
    int FilesLength = files.Length;
    Dictionary<string, int>[] Dictionarys = new Dictionary<string, int>[FilesLength];

    for (int i = 0; i < FilesLength; i++)
    {
        string[] CurrentFile = File.ReadAllText(files[i]).Split();
        Dictionary<string, int> CurrentDict = new Dictionary<string, int>();

        foreach (string word in CurrentFile)
        {
            if (CurrentDict.Keys.Contains(word))
                CurrentDict[word] += 1;
            else
                CurrentDict[word] = 1;
        }
        Dictionarys[i] = CurrentDict;
    }
    return Dictionarys;
}

int[,] GetMatrix(string[] files, Dictionary<string, int>[] Dictionarys, List<string> AllTheWords)
{
    int[,] Matrix = new int[files.Length, AllTheWords.Count];

    for (int x = 0; x < files.Length; x++)
    {
        foreach (string s in Dictionarys[x].Keys)
        {
            int y = AllTheWords.IndexOf(s);
            Matrix[x, y] = Dictionarys[x][s];
        }
    }
    return Matrix;
}


int[] Sentence2Vec(string sentence, List<string> AllTheWords)
{
    string[] sentenceArray = sentence.Split();
    int[] vec = new int[AllTheWords.Count];

    for (int i = 0; i < sentenceArray.Length; i++)
    {
        if (AllTheWords.Contains(sentenceArray[i]))
            vec[AllTheWords.IndexOf(sentenceArray[i])] = 1;
    }

    return vec;
}


// MAIN: Gets a tfidf Matrix out of the WordsXFiles Matrix
double[,] tf_idf(int[,] Matrix)
{
    double[,] tf_idf = new double[Matrix.GetLength(0), Matrix.GetLength(1)];

    for (int x = 0; x < tf.GetLength(0); x++)
    {
        for (int y = 0; y < tf.GetLength(1); y++)
        {
            tf_idf[x, y] = Matrix[x, y] / TotalWords[x] * Math.Log(Matrix.GetLength(0) / GetNumberOfDocsEachWordAppears[y]);
        }
    }
    return tf_idf;

    int[] GetTotalWords(int[,] Matrix)
    {
        int[] TotalWords = new int[Matrix.GetLength(0)];

        for (int x = 0; x < Matrix.GetLength(0); x++)
        {
            int sum = 0;
            for (int y = 0; y < Matrix.GetLength(1); y++)
            {
                sum += Matrix[x, y];
            }
            TotalWords[x] = sum;
        }
        return TotalWords;
    }

    List<string> GetAllTheWords(Dictionary<string, int>[] Dictionarys)
    {
        List<string> AllTheWords = new List<string>();
        for (int i = 0; i < Dictionarys.Length; i++)
        {
            foreach (string s in Dictionarys[i].Keys)
            {
                if (!AllTheWords.Contains(s))
                    AllTheWords.Add(s);
            }
        }
        return AllTheWords;
    }

    int[] GetNumberOfDocsEachWordAppears(int[,] Matrix)
    {
        int[] NumberOfDocsEachWordAppears = new int[Matrix.GetLength(1)];

        for (int y = 0; y < tf.GetLength(0); y++)
        {
            for (int x = 0; x < tf.GetLength(1); x++)
            {
                if (Matrix[x, y] != 0)
                    NumberOfDocsEachWordAppears[y] += 1;
            }
        }
        return NumberOfDocsEachWordAppears;
    }
}

// AUXILIARI

void PrintMatrix(int[,] Matrix)
{
    for (int x = 0; x < Matrix.GetLength(0); x++)
    {
        for (int y = 0; y < Matrix.GetLength(1); y++)
            System.Console.Write(Matrix[x, y] + "  ");
        System.Console.WriteLine();
    }
}

// OLD MOOGLE


        // Dictionary<string, int> query_and_docs_occur = Query_and_docs_occur(search_query, Dictionarys);

        // SearchItem[] FilesOccur = new SearchItem[files.Length];

        // for (int i = 0; i < files.Length; i++)
        // {
        //     // Method: Construir el SearchItem
        //     var text = Dictionarys[i];
        //     string CurrentFile = File.ReadAllText(files[i]);
        //     int FileSize = text.Values.Sum();
        //     string FileName = Path.GetFileNameWithoutExtension(files[i]);
        //     string snippet = GetSnip(CurrentFile, search_query);

        //     double Score = tf_idf(query_and_docs_occur, text,Importance, FileSize, files.Length, search_query) * (1 + 1 / GetCLoseWords(CurrentFile, query, text));

        //     FilesOccur[i] = new SearchItem(FileName, snippet, (float)Score);
        // tf(count Occur,CurrentFile)
        // idf(files.Length,docsOccur)
    

    // var items = ResultsPreparations.SortSearchItems(FilesOccur);

    // foreach (SearchItem item in items)
    //     System.Console.WriteLine(item);

    // timer.Stop();
    // System.Console.WriteLine("Time taken: " + timer.Elapsed.ToString(@"m\:ss\.fff"));
    // timer.Restart();

    // timer.Start();
    // var suggestion = "Suggestion.GetSuggestion(files,search_query)";
    // // var suggestion = Suggestion.GetSuggestion(files,search_query);
    // timer.Stop();
    // System.Console.WriteLine("Time taken in suggestion: " + timer.Elapsed.ToString(@"m\:ss\.fff"));

    // return new SearchResult(items, suggestion);// Suggestion.GetSuggestion(files,search_query)





    // public static double tf_idf(Dictionary<string, int> Query_docs_occur, Dictionary<string, int> text,Dictionary<string, int> Importance, int words_in_text, int docs_in_corpus, string query)
    // {
    //     double tf_idf = 0;
    //     var imp = 1;
    //     List<string> NotAllowedWords = GetNotAllowedWords(query);
    //     List<string> NecessaryWords = GetNecessaryWords(query);

    //     foreach (string word in NecessaryWords)
    //     {
    //         if (!text.Keys.Contains(word))
    //             return tf_idf;
    //     }
    //     foreach (string word in NotAllowedWords)
    //     {
    //         if (text.Keys.Contains(word))
    //             return tf_idf;
    //     }
    //     foreach (string word in Query_docs_occur.Keys)
    //     {
    //         if(Importance.ContainsKey(word))
    //             imp += Importance[word];
    //         System.Console.WriteLine(word);
    //         System.Console.WriteLine(Moogle.tf(word, text) * Moogle.idf(docs_in_corpus, Query_docs_occur[word])*imp);
    //         // System.Console.WriteLine(imp);
    //         tf_idf += Moogle.tf(word, text) * Moogle.idf(docs_in_corpus, Query_docs_occur[word])*imp;
    //         imp = 1;
    //     }

    //     return tf_idf;
    // }
    // public static double tf(string subs, Dictionary<string, int> Dictionary)
    // {
    //     int a = 0;
    //     if (Dictionary.ContainsKey(subs))
    //         a = Dictionary[subs];
    //     return ((double)a / (double)Dictionary.Count);
    // }


    // public static double idf(int docs_in_corpus, int docs_with_occur)
    // {
    //     if (docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
    //         return 0;
    //     return Math.Log(10, (double)docs_in_corpus / (double)docs_with_occur);
    // }

    // public static List<string> GetNotAllowedWords(string query)
    // {
    //     string[] query_array = query.Split();
    //     List<string> GetNotAllowedWords = new List<string>();
    //     if (query == "")
    //         return GetNotAllowedWords;
    //     foreach (string word in query_array)
    //     {
    //         if (word[0].ToString() == "!")
    //             GetNotAllowedWords.Add(word.Substring(1));
    //     }
    //     return GetNotAllowedWords;
    // }
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


    // }
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
