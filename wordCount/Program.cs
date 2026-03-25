using System.Text;
using System.Text.RegularExpressions;

internal class Program
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
        var filePath = Path.Combine("..", "..", "..", "text.txt");


        if (!File.Exists(filePath))
        {
            return;
        }
        var textList = new List<string>();
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                textList.Add(line);
            }
        }
        var text = textList.ToArray();

        string word = "он";
    }
    static long WordCount_IndexOf(string[] text, string word)
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



    static long WordCount_Linq(string[] text, string word)
    {
        return text
    .SelectMany(t => t.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
    .Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));

    }

    static long WordCount_Binary(string[] text, string word)
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
    static long WordCount_Regex(string[] text, string word)
    {
        var pattern = $@"(?<![а-яёА-ЯЁa-zA-Z0-9]){Regex.Escape(word)}(?![а-яёА-ЯЁa-zA-Z0-9])";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        return text
        .SelectMany(line => regex.Matches(line)
            .Cast<Match>()
            .Select(m => m.Value))
        .Count(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
    }
    static long WordCount_Custom(string[] text, string word)
    {
        var count = 0L;
        var currentWord = new StringBuilder();
        foreach (var line in text)
        {
            currentWord.Clear();

            for (int i = 0; i <= line.Length; i++)
            {
                char c = i < line.Length ? line[i] : '\0';
                bool isSeparator = i == line.Length || Array.IndexOf(Separators, c) >= 0;

                if (!isSeparator)
                {
                    currentWord.Append(c);
                }
                else if (currentWord.Length > 0)
                {
                    if (string.Equals(currentWord.ToString(), word, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                    currentWord.Clear();
                }
            }
        }

        return count;
    }
}