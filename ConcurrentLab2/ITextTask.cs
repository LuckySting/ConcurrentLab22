using System.Linq;

namespace ConcurrentLab2
{
    public abstract class ITextTask
    {
        public string ClearStr(string input)
        {
            var clearStr = new string(input
                .Where(l => !char.IsPunctuation(l) && !char.IsDigit(l) && !char.IsSeparator(l) && l != '\t' && l != '\r').ToArray());
            return clearStr.ToLower();
        }

        public abstract string MostLongWord(string[] filesPaths);
        public abstract string[] MostFrequentWords(string[] filesPaths, int wordsCount = 10);
        public abstract string MostCommonFile(string[] filesPaths);
    }
}