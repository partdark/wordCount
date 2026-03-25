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
}