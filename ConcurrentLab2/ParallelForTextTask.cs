using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentLab2
{
    public class ParallelForTextTask : ITextTask
    {
        public override string MostLongWord(string[] filesPaths)
        {
            string result = "";
            Parallel.ForEach(Partitioner.Create(filesPaths, true),
                () => "",
                (path, state1, partial1) =>
                {
                    string longestWord = "";
                    foreach (var word in File.ReadAllText(path).Split(" "))
                    {
                        var clearWord = ClearStr(word);
                        if (clearWord.Length > longestWord.Length)
                        {
                            longestWord = clearWord;
                        }
                    }

                    if (longestWord.Length > partial1.Length)
                    {
                        partial1 = longestWord;
                    }

                    return partial1;
                },
                partial1 =>
                {
                    if (partial1.Length > result.Length)
                    {
                        result = partial1;
                    }
                }
            );

            return result;
        }

        public override string[] MostFrequentWords(string[] filesPaths, int wordsCount = 10)
        {
            ConcurrentDictionary<string, int> wordsFreq = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(
                Partitioner.Create(filesPaths, true),
                path =>
                {
                    foreach (var word in File.ReadAllText(path).Split(" "))
                    {
                        var clearWord = ClearStr(word);
                        wordsFreq.AddOrUpdate(clearWord, (s) => 1, (s, i) => i + 1);
                    }
                }
            );
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
            var wordsFreq = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();
            Parallel.ForEach(
                Partitioner.Create(filesPaths, true),
                path =>
                {
                    foreach (var word in File.ReadAllText(path).Split(" "))
                    {
                        var clearWord = ClearStr(word);
                        wordsFreq.AddOrUpdate(clearWord,
                            (w) =>
                            {
                                return new ConcurrentDictionary<string, int>(new[]
                                    {new KeyValuePair<string, int>(path, 1)});
                            },
                            (w, d) =>
                            {
                                d.AddOrUpdate(path, (p) => 1, (p, i) => i + 1);
                                return d;
                            });
                    }
                }
            );

            var wordsSum = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(
                wordsFreq,
                wordFreq =>
                {
                    if (wordFreq.Value.Count() == filesPaths.Length)
                    {
                        Parallel.ForEach(
                            wordFreq.Value,
                            fileFreq =>
                            {
                                wordsSum.AddOrUpdate(
                                    fileFreq.Key,
                                    (s) => fileFreq.Value,
                                    (s, i) => i + fileFreq.Value
                                );
                            }
                        );
                    }
                }
            );

            var wordsSumArr = wordsSum.ToArray();
            Array.Sort(wordsSumArr, (a, b) => b.Value - a.Value);
            return wordsSumArr[0].Key;
        }
    }
}