namespace MoogleEngine;

public class WordDictionarys
{
    public static Dictionary<string, int>[] GetWordDictionarys(string[] files)
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
}