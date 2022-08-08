using System.IO;
using System.Text.RegularExpressions;

namespace MoogleEngine;

public static class Moogle
{
    // public static SearchItem Max(SearchItem[] array)//Este Metodo No es Necesario
    // {
    //     SearchItem Max = array[0];
    //     for (int i = 0; i < array.Length; i++)
    //     {
    //         if (array[i].Score > Max.Score)
    //             Max = array[i];
    //     }
    //     return Max;
    // }

    //Arreglar el snip
    //No encuentra palabras acompannadas  de caracteres especiales(:,#,.,etc...)
    // Hacer Sugerncias
    public static SearchResult Query(string query)//Este es el metodo PrincipalMAIN METHOD
    {
        // Modifique este método para responder a la búsqueda

        string content = @"E:\Prog\00moogle\moogle-main\Content";
        string[] files = Directory.GetFiles(content);
        string search_query = erase_whitespace(erase_stopwords(query));
        string[] search_query_array = search_query.Split();
        Dictionary<string, int> query_and_docs_occur = Query_and_docs_occur(search_query_array, files);

        SearchItem[] FilesOccur = new SearchItem[files.Length];

        query = erase_whitespace(query);

        for (int i = 0; i < files.Length; i++)
        {
            // CurrentFile.Length para el tf
            string CurrentFile = File.ReadAllText(files[i]);
            int FileSize = CurrentFile.Split().Length;
            string FileName = Path.GetFileNameWithoutExtension(files[i]);
            string snippet = GetSnip(CurrentFile, query);
            double Score = tf_idf(query_and_docs_occur, CurrentFile, FileSize, files.Length, search_query);

            FilesOccur[i] = new SearchItem(FileName, snippet, (float)Score);
            // tf(count Occur,CurrentFile)
            // idf(files.Length,docsOccur)
        }

        FilesOccur = DescendingSort(FilesOccur);
        int ScoreZero = FilesOccur.Length;

        for (int i = 0; i < FilesOccur.Length; i++)
        {
            if (FilesOccur[i].Score == 0)
            {
                ScoreZero = i;
                break;
            }
        }

        SearchItem[] items = new SearchItem[ScoreZero];
        for (int i = 0; i < items.Length; i++)
        {
            // items[i] = FilesOccur[0];
            items[i] = FilesOccur[i];
        }

        // foreach(SearchItem item in items)
        //     System.Console.WriteLine(item);


        return new SearchResult(items, query);
    }
    public static double tf_idf(Dictionary<string, int> Query_docs_occur, string text, int words_in_text, int docs_in_corpus, string query)
    {
        double tf_idf = 0;
        List<string> NotAllowedWords = GetNotAllowedWords(query);
        List<string> NecessaryWords = GetNecessaryWords(query);

        foreach (string word in NecessaryWords)
        {
            if (!text.Contains(word))
                return tf_idf;
        }
        foreach (string word in NotAllowedWords)
        {
            if (text.Contains(word))
                return tf_idf;
        }
        foreach (string word in Query_docs_occur.Keys)
        {
            tf_idf += Moogle.tf(text, word) * Moogle.idf(docs_in_corpus, Query_docs_occur[word]);// + 1/GetCLoseWords(text, query);
        }

        return tf_idf;
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
    static public int GetCLoseWords(string text, string query)
    {
        string pattern = @"[ ~ ]||[~]||[ ~]||[~ ]";
        Regex obj = new Regex(pattern);

        List<int> distances = new List<int>();
        int distance = 0;
        int ans = 0;
        string[] query_array = query.Split();

        for (int i = 0; i < query_array.Length - 2; i++)
        {
            int LeftWordIndex = text.IndexOf(query_array[i]);
            int RightWordIndex = text.IndexOf(query_array[i + 2]);

            if (obj.IsMatch(query_array[i + 1]) && LeftWordIndex >= 0 && RightWordIndex >= 0)
            {
                distance = (Math.Abs(LeftWordIndex - RightWordIndex));
                distances.Add(distance);
            }
        }
        foreach (int e in distances)
            ans += e;

        return ans;
    }
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
    public static string erase_stopwords(string sentence)//This method erases stopwords from the queryQUERY PROCESSING
    {
        string[] array_sentence = sentence.Split();
        string ans = "";

        for (int i = 0; i < array_sentence.Length; i++)
        {
            if (!Is_stop_word(array_sentence[i]))
                ans = ans + array_sentence[i] + " ";
        }
        return ans;
    }
    public static string erase_whitespace(string sentence)//this method erases whitespace from the queryQUERY PROCESSING
    {
        string ans = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            if (!(sentence[i].ToString() == " " && ((i + 1 == sentence.Length) || (sentence[i + 1].ToString() == " "))))
                ans = ans + sentence[i];
        }
        return ans;
    }
    public static bool Is_stop_word(string word)//this method dtermines if a word is a stopwordQUERY PROCESSING
    {
        string stop_words_archive = @"E:\Prog\00moogle\moogle-main\stopwords.txt";
        string[] stopwords = File.ReadAllLines(stop_words_archive);

        if (stopwords.Contains(word.ToLower()))
            return true;
        return false;
    }
    //Cuenta las ocurrencias de una palabra en un texto
    public static int RawCount(string text, string subs)
    {
        if (subs == "")
            return 0;//No pincha porque el substring es ""
        int occurrences = 0;
        int start = text.IndexOf(subs);

        text = text.ToLower();
        subs = subs.ToLower();

        if (start != -1)
        {
            occurrences++;
            occurrences = occurrences + RawCount(text.Substring(start + subs.Length), subs);
        }
        return occurrences;
    }

    public static double tf(string text, string subs)
    {
        string[] text_array = text.Split();
        return ((double)RawCount(text, subs) / (double)(text_array.Length));
    }

    public static double idf(int docs_in_corpus, int docs_with_occur)
    {
        if (docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
            return 0;
        return Math.Log(10, (double)docs_in_corpus / (double)docs_with_occur);
    }

    // This method returns a dict with each word in the query and the amount of files in wich it appears
    public static Dictionary<string, int> Query_and_docs_occur(string[] search_query_array, string[] files)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);

        foreach (string word in search_query_array)
            dict.Add(word, 0);

        for (int i = 0; i < files.Length; i++)
        {
            string CurrentFile = File.ReadAllText(files[i]);

            foreach (string word in search_query_array)
            {
                if (word == "" || word == " ")
                    continue;
                else if (CurrentFile.Contains(word))
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


    //Cuenta las ocurrencias de cada palabra de una oracion en un texto y las suma
    public static int SentenceCountOc(string text, string sentence)
    {
        string[] words = sentence.Split();
        int totalOccur = 0;

        foreach (string word in words)
        {
            // TFidf = tf(text, word) * idf(word)
            totalOccur += Moogle.RawCount(text, word);
        }

        return totalOccur;
    }
    //Ordena Un Array De SearchItems de mayor a menor segun el Score RESULTS PROCESSING
    public static SearchItem[] DescendingSort(SearchItem[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j].Score > array[i].Score)
                {
                    SearchItem max = array[j];
                    array[j] = array[i];
                    array[i] = max;
                }
            }
        }
        return array;
    }

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



// 0.01905176112949586
// 0.004127538426740262
// 0.0005930665979622655
// 0.0027132793128165906
// 0.00010239393220392612
// 0.0004787893682015874
// 0.001105896176125092
// 0.0013115821798971503
// 0.0026231643597943006