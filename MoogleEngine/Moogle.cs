using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MoogleEngine;

public static class Moogle
{
    public static SearchResult Query(string query)//Este es el metodo PrincipalMAIN METHOD
    {

        string content = @"E:\Prog\moogle\moogle-main\Content";


        // var Importance = GetImportance(query);
        string search_query = QueryProcessing.ProcessQuery(query);

        string[] files = Directory.GetFiles(content);
        var Dictionarys = MatricesWork.GetWordDictionarys(files);
        var AllTheWords = MatricesWork.GetAllTheWords(Dictionarys);
        var QueryVec = MatricesWork.Sentence2Vec(search_query, AllTheWords);
        var PreMatrix = MatricesWork.GetMatrix(files, Dictionarys, AllTheWords);
        var Matrix = MatricesWork.tf_idf(PreMatrix);
        var SearchValues = MatricesWork.VecXMatrix(QueryVec, Matrix);
        SearchItem[] searchItems = GetSearchItems(SearchValues);
        string Suggestion = "GetSuggestion";
        SearchResult result = new SearchResult(searchItems, Suggestion);



        return result;


        SearchItem[] GetSearchItems(double[] SearchValues)
        {
            List<SearchItem> items = new List<SearchItem>();

            // SearchItem(string title, string snippet, float score)
            for (int i = 0; i < SearchValues.Length; i++)
            {
                string FileName = "";
                string Snippet = "";
                float Score = 0;
                if (SearchValues[i] != 0)
                {
                    FileName = Path.GetFileNameWithoutExtension(files[i]);
                    Snippet = GetSnip(files[i], search_query);
                    Score = (float)SearchValues[i];
                    SearchItem item = new(FileName, Snippet, Score);
                    items.Add(item);
                }
            }
            var itemArray = ResultsPreparations.SortSearchItems(items.ToArray());
            return itemArray;
        }

    }
    public static string GetSnip(string filePath, string query)//Este metodo coge el snip (Es mejorable)ARREGLAR
    {
        string[] query_array = query.Split();
        string text = File.ReadAllText(filePath);
        int middle = -1;
        int SnipLeng = 500;

        foreach (string word in query_array)
        {
            middle = text.IndexOf(word);
            if (middle >= 0)
            {
                if (text.Length <= SnipLeng)
                    return text;
                if (text.Length - middle <= SnipLeng)
                    return text.Substring(middle, text.Length - middle);

                if (middle < 250)
                    return text.Substring(0, SnipLeng);
                else
                    return text.Substring(middle - 250, SnipLeng);
            }
        }
        return " ";
    }



    static double GetCLoseWords(string text, string query, Dictionary<string, int> Dict)
    {
        string pattern = @"[ ~ ]||[~]||[ ~]||[~ ]";
        Regex obj = new Regex(pattern);

        List<int> distances = new List<int>();

        string[] query_array = query.Split();

        if (query_array.Length >= 3)
        {
            for (int i = 0; i < query_array.Length - 2; i++)
            {
                string LeftWord = query_array[i];
                string RightWord = query_array[i + 2];

                if (obj.IsMatch(query_array[i + 1]) && Dict.ContainsKey(LeftWord) && Dict.ContainsKey(RightWord))
                {
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
                }
            }
            if (distances.Count >= 1)
            {
                int ans = distances[0];
                foreach (int dist in distances)
                {
                    if (dist < ans)
                        ans = dist;
                }
                return ans;
            }
        }
        return 1;
    }

    static int[] GetIndexes(string text, string word, int cant)
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


    static Dictionary<string, int> Query_and_docs_occur(string search_query, Dictionary<string, int>[] Dictionarys)
    {
        string[] search_query_array = search_query.Split();
        Dictionary<string, int> dict = new Dictionary<string, int>(search_query_array.Length);

        foreach (string word in search_query_array)
        {
            if (!dict.ContainsKey(word))
                dict.Add(word, 0);
        }
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
    static double[,] NotAllowedandNecessaryOp(string query, List<string> AllTheWords, double[,] Matrix)
    {
        string[] query_array = query.Split();
        List<string> NecessaryWords = new List<string>();
        List<string> NotAllowedWords = new List<string>();

        if (query == "")
            return Matrix;

        foreach (string word in query_array)
        {
            if (word[0].ToString() == "^")
                NecessaryWords.Add(word.Substring(1));
            if (word[0].ToString() == "!")
                NotAllowedWords.Add(word.Substring(1));
        }

        List<int> Necessaryindexes = new List<int>();
        List<int> NotAllowedindexes = new List<int>();

        foreach (string word in NecessaryWords)
        {
            if (AllTheWords.Contains(word))
            {
                Necessaryindexes.Add(AllTheWords.IndexOf(word));
            }
        }
        foreach (string word in NotAllowedWords)
        {
            if (AllTheWords.Contains(word))
            {
                NotAllowedindexes.Add(AllTheWords.IndexOf(word));
            }
        }

        foreach (int i in Necessaryindexes)
        {
            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                if (Matrix[x, i] == 0)
                {
                    for (int y = 0; y < Matrix.GetLength(1); y++)
                        Matrix[x, y] = 0;
                }
            }
        }
        foreach (int i in NotAllowedindexes)
        {
            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                if (Matrix[x, i] != 0)
                {
                    for (int y = 0; y < Matrix.GetLength(1); y++)
                        Matrix[x, y] = 0;
                }
            }
        }

        return Matrix;
    }


    static double[,] GetImportance(string query,List<string> AllTheWords, double[,] Matrix)
    {
        string pattern = @"^\*";
        Regex obj = new Regex(pattern);

        Dictionary<string, int> Importance = new Dictionary<string, int>();
        string[] query_array = query.Split();

        foreach (string word in query_array)
        {
            if (obj.IsMatch(word))
            {
                int asterisc = 0;
                int index = 0;
                while (word[index].ToString() == "*")
                {
                    index += 1;
                    asterisc += 1;
                }
                Importance[word.Substring(index)] = asterisc;
            }
        }

        foreach(string word in Importance.Keys)
        {
            int index = 0;
            int raise = Importance[word];
            if(AllTheWords.Contains(word))
            {
                index = AllTheWords.IndexOf(word);
                for(int x=0; x<Matrix.GetLength(0);x++)
                {
                    Matrix[x,index] = (double)raise*Matrix[x,index];
                } 
            }
        }
        return Matrix;
    }
}