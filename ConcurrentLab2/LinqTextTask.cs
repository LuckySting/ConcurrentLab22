using System.IO;
using System.Linq;

namespace ConcurrentLab2
{
    public class LinqTextTask : ITextTask
    {
        public override string MostLongWord(string[] filesPaths)
        {
            return filesPaths
                .Select(path => File.ReadAllText(path))
                .SelectMany(text => text.Split(" "))
                .Select(ClearStr)
                .OrderByDescending(word => word.Length).First();
        }

        public override string[] MostFrequentWords(string[] filesPaths, int wordsCount = 10)
        {
            return filesPaths
                .Select(path => File.ReadAllText(path))
                .SelectMany(text => text.Split(" "))
                .Select(ClearStr)
                .GroupBy(word => word)
                .Select(group => new {group.Key, Value = group.Count()})
                .OrderByDescending(pair => pair.Value).Select(pair => pair.Key)
                .Take(wordsCount).ToArray();
        }

        public override string MostCommonFile(string[] filesPaths)
        {
            var wordsFreq =
                (from wrdCnt in (
                        from pWord in (
                            from path in filesPaths
                            from word in File.ReadAllText(path).Split(" ")
                            select new {path, word = ClearStr(word)}
                        )
                        group pWord by new {pWord.path, pWord.word}
                        into g
                        select new {g.Key.path, g.Key.word, wrdCnt = g.Count()}
                    )
                    group wrdCnt by wrdCnt.word
                    into g
                    select new
                    {
                        g.Key,
                        words = g
                            .Select(src => new {src.path, src.wrdCnt})
                            .ToDictionary(src => src.path, src => src.wrdCnt)
                    }).ToDictionary(src => src.Key, src => src.words);
            return (
                from path in filesPaths
                select new
                {
                    path,
                    sum = (
                        from wordFreq in wordsFreq
                        where wordFreq.Value.Count() == filesPaths.Length
                        select wordFreq.Value[path]
                    ).Sum(src => src)
                }).OrderByDescending(src => src.sum).First().path;
        }
    }
}