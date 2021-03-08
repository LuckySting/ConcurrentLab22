using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConcurrentLab2
{
    public class SerialTextTask : ITextTask
    {
        public override string MostLongWord(string[] filesPaths)
        {
            int maxLength = 0;
            string longestWord = "";
            foreach (var path in filesPaths)
            {
                foreach (var word in File.ReadAllText(path).Split(" "))
                {
                    var clearWord = ClearStr(word);
                    if (clearWord.Length > maxLength)
                    {
                        longestWord = clearWord;
                        maxLength = clearWord.Length;
                    }
                }
            }

            return longestWord;
        }

        public override string[] MostFrequentWords(string[] filesPaths, int wordsCount = 10)
        {
            Dictionary<string, int> wordsFreq = new Dictionary<string, int>();
            foreach (var path in filesPaths)
            {
                foreach (var word in File.ReadAllText(path).Split(" "))
                {
                    var clearWord = ClearStr(word);
                    if (wordsFreq.ContainsKey(clearWord))
                    {
                        wordsFreq[clearWord]++;
                    }
                    else
                    {
                        wordsFreq[clearWord] = 1;
                    }
                }
            }

            KeyValuePair<string, int>[] wordsList = wordsFreq.ToArray();
            Array.Sort(wordsList, (a, b) => b.Value - a.Value);
            string[] ans = new string[10];
            for (int i = 0; i < 10; i++)
            {
                ans[i] = wordsList[i].Key;
            }

            return ans;
        }

        public override string MostCommonFile(string[] filesPaths)
        {
            var wordsFreq = new Dictionary<string, Dictionary<string, int>>();
            foreach (var path in filesPaths)
            {
                foreach (var word in File.ReadAllText(path).Split(" "))
                {
                    var clearWord = ClearStr(word);
                    if (wordsFreq.ContainsKey(clearWord))
                    {
                        if (wordsFreq[clearWord].ContainsKey(path))
                        {
                            wordsFreq[clearWord][path]++;
                        }
                        else
                        {
                            wordsFreq[clearWord][path] = 1;
                        }
                    }
                    else
                    {
                        wordsFreq[clearWord] = new Dictionary<string, int>();
                        wordsFreq[clearWord][path] = 1;
                    }
                }
            }

            var wordsSum = new Dictionary<string, int>();

            foreach (var word in wordsFreq.Keys)
            {
                if (wordsFreq[word].Count() < filesPaths.Length)
                {
                    wordsFreq.Remove(word);
                }
                else
                {
                    foreach (var fileFreq in wordsFreq[word])
                    {
                        if (wordsSum.ContainsKey(fileFreq.Key))
                        {
                            wordsSum[fileFreq.Key] += fileFreq.Value;
                        }
                        else
                        {
                            wordsSum[fileFreq.Key] = fileFreq.Value;
                        }
                    }
                }
            }

            var wordsSumArr = wordsSum.ToArray();
            Array.Sort(wordsSumArr, (a, b) => b.Value - a.Value);
            return wordsSumArr[0].Key;
        }
    }
}