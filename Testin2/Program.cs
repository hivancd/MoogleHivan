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

void PrintMatrix(int[,] Matrix)
{
    for (int x = 0; x < Matrix.GetLength(0); x++)
    {
        for (int y = 0; y < Matrix.GetLength(1); y++)
            System.Console.Write(Matrix[x, y] + "  ");
        System.Console.WriteLine();
    }
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


string content = @"E:\Prog\moogle\moogle-main\Content";
string[] files = Directory.GetFiles(content);


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


// void WriteDict(Dictionary<string, int> Dict)
// {
//     foreach (string s in Dict.Keys)
//     {
//         System.Console.Write("{" + s + ", " + Dict[s] + "} ");
//     }
//     System.Console.WriteLine();
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
