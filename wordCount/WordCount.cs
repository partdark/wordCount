using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Running;


namespace WordCount
{
    public class WordCountMain
    {

        public static char[] Separators = {
    ' ', '\t', '\n', '\r',
      '.', ',', '!', '?', ';', ':', '-',
    '(', ')', '[', ']', '{', '}',
    '«', '»',
    '—', '–',
    '/', '\\',
    '"', '\'', '`',
    '_', '*', '#', '@', '&', '%', '+', '=',
    '<', '>', '|', '^', '~' };
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkFinding>();
            Console.ReadKey();
        }
      public  static long WordCount_IndexOf(string[] text, string word)
        {
            var count = 0L;

            for (var i = 0; i < text.Length; i++)
            {
                var index = 0;
                while (true)
                {
                    var sub = text[i].IndexOf(word, index, StringComparison.OrdinalIgnoreCase);
                    if (sub == -1)
                    {
                        break;
                    }


                    var isBeforeValid = (sub == 0) || !char.IsLetterOrDigit(text[i][sub - 1]);


                    var afterIndex = sub + word.Length;
                    var isAfterValid = (afterIndex == text[i].Length) ||
                                        !char.IsLetterOrDigit(text[i][afterIndex]);

                    if (isBeforeValid && isAfterValid)
                    {
                        ++count;
                    }

                    index = sub + 1;
                    if (index >= text[i].Length)
                    {
                        break;
                    }
                }
            }
            return count;
        }



        public long WordCount_Linq(string[] text, string word)
        {
            return text
        .SelectMany(t => t.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
        .Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));

        }

        public  long WordCount_Binary(string[] text, string word)
        {
            var splitedText = text
         .SelectMany(t => t.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
         .ToArray();
            Array.Sort(splitedText, StringComparer.OrdinalIgnoreCase);



            var count = 0L;
            var index = Array.BinarySearch(splitedText, word, StringComparer.OrdinalIgnoreCase);
            if (index < 0)
            {
                return count;
            }
            count++;
            var left = index - 1;
            while (left >= 0 && String.Equals(splitedText[left], word, StringComparison.OrdinalIgnoreCase))
            {
                count++;
                left--;
            }
            var right = index + 1;
            while (right < splitedText.Length && String.Equals(splitedText[right], word, StringComparison.OrdinalIgnoreCase))
            {
                count++;
                right++;
            }

            return count;

        }
        public  long WordCount_Regex(string[] text, string word)
        {
            var pattern = $@"(?<![а-яёА-ЯЁa-zA-Z0-9]){Regex.Escape(word)}(?![а-яёА-ЯЁa-zA-Z0-9])";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            return text
            .SelectMany(line => regex.Matches(line)
                .Cast<Match>()
                .Select(m => m.Value))
            .Count(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
        }
       
    }
}