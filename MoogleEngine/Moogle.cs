using System.IO;

namespace MoogleEngine;

public static class Moogle
{
    // TODO: Encontrar Frecuencia en el corpus
    // TODO: Encontrar Repeticiones en el archivo
    // Calcular TFIDF y comparar, coger 3 mas altos
    // Hacer Sugerncias

    public static SearchItem[] DescendingSort(SearchItem[] array)//Ordena Un Array De SearchItems de mayor a menor segun el Score
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

    public static int CountOcurrences(string text, string subs)//Cuenta las ocurrencias de una palabra en un texto
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

    public static int SentenceCountOc(string text, string sentence)//Cuenta las ocurrencias de cada palabra de 
                                                                   // una oracion en un texto y las suma 
    {
        string[] words = sentence.Split();
        int totalOccur = 0;

        foreach (string word in words)
        {
            totalOccur = totalOccur + Moogle.CountOcurrences(text, word);
        }

        return totalOccur;
    }

    public static string GetSnip(string text, string word)//Este metod coge el snip (Es mejorable)
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

    public static SearchResult Query(string query)//Este es el metodo Principal
    {
        // Modifique este método para responder a la búsqueda

        string content = @"E:\Prog\00moogle\moogle-main\Content";
        string[] files = Directory.GetFiles(content);
        SearchItem[] FilesOccur = new SearchItem[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            string CurrentFile = File.ReadAllText(files[i]);
            string FileName = Path.GetFileNameWithoutExtension(files[i]);
            // string snippet = "Fill the snippet dont be lazy";
            FilesOccur[i] = new SearchItem(FileName, GetSnip(CurrentFile, query), SentenceCountOc(CurrentFile, query));
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
