using System.IO;

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



    public static string GetSnip(string text, string word)//Este metodo coge el snip (Es mejorable)ARREGLAR
    {
        int middle = text.IndexOf(word);
        int SnipLeng = 30;

        if (middle > 0)
        {
            if (text.Length - middle <= SnipLeng)
                return text.Substring(middle, text.Length);
            if (middle < 15)
                return text.Substring(0, SnipLeng);
            else
                return text.Substring(middle - 15, SnipLeng);
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
    public static int CountOcurrences(string text, string subs)
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
            occurrences = occurrences + CountOcurrences(text.Substring(start + subs.Length), subs);
        }
        return occurrences;
    }

    public static double tf(int raw_count, string text)
    {
        string[] text_array = text.Split();
        return ((double)raw_count / (double)(text_array.Length));
    }

    public static double idf(int docs_in_corpus, int docs_with_occur)
    {
        if (docs_with_occur == 0 || docs_in_corpus == docs_with_occur)
            return 0;
        return Math.Log(10, (double)docs_in_corpus / (double)docs_with_occur);
    }
    //Cuenta las ocurrencias de cada palabra de una oracion en un texto y las suma
    public static int SentenceCountOc(string text, string sentence)                                                               
    {
        string[] words = sentence.Split();
        int totalOccur = 0;

        foreach (string word in words)
        {
            // TFidf = tf(text, word) * idf(word)
            totalOccur += Moogle.CountOcurrences(text, word);
        }

        return totalOccur;
    }
    public static SearchItem[] DescendingSort(SearchItem[] array)//Ordena Un Array De SearchItems de mayor a menor segun el ScoreRESULTS PROCESSING
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
    public static SearchResult Query(string query)//Este es el metodo PrincipalMAIN METHOD
    {
        // Modifique este método para responder a la búsqueda

        string content = @"E:\Prog\00moogle\moogle-main\Content";
        string[] files = Directory.GetFiles(content);
        SearchItem[] FilesOccur = new SearchItem[files.Length];
        string search_query = erase_whitespace(erase_stopwords(query));
        query = erase_whitespace(query);

        for (int i = 0; i < files.Length; i++)
        {
            string CurrentFile = File.ReadAllText(files[i]);
            string FileName = Path.GetFileNameWithoutExtension(files[i]);
            string snippet = "Fix the snippet dont be lazy";//GetSnip(CurrentFile, query)
            FilesOccur[i] = new SearchItem(FileName, snippet, SentenceCountOc(CurrentFile, search_query));
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
        return new SearchResult(items, query);
    }
}
