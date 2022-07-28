using System.IO;

namespace MoogleEngine;

public static class Moogle
{
    // TODO: Encontrar Frecuencia en el corpus
    // TODO: Encontrar Repeticiones en el archivo
    // Calcular TFIDF y comparar, coger 3 mas altos
    // Hacer Sugerncias

    public static SearchItem[] DescendingSort(SearchItem[] array)//Solo funciona con arrays de  numeros positivos
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
    public static SearchItem Max(SearchItem[] array)
    {
        SearchItem Max = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Score > Max.Score)
                Max = array[i];
        }
        return Max;
    }

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

    public static int SentenceCountOc(string text, string sentence)
    {
        string[] words = sentence.Split();
        int totalOccur = 0;

        foreach (string word in words)
        {
            totalOccur = totalOccur + Moogle.CountOcurrences(text, word);
        }

        return totalOccur;
    }
    public static SearchResult Query(string query)
    {
        // Modifique este método para responder a la búsqueda

        string content = @"E:\Prog\00moogle\moogle-main\Content";
        string[] files = Directory.GetFiles(content);
        SearchItem[] FilesOccur = new SearchItem[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            string CurrentFile = File.ReadAllText(files[i]);
            string FileName = Path.GetFileNameWithoutExtension(files[i]);
            string snippet = "Fill the snippet dont be lazy";
            FilesOccur[i] = new SearchItem(FileName, snippet, SentenceCountOc(CurrentFile, query));
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
