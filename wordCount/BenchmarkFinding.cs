namespace WordCount
{
    using BenchmarkDotNet.Attributes;
    using WordCount;
    using static WordCount.WordCountMain;

    [MemoryDiagnoser]
    public class BenchmarkFinding
    {
        WordCount.WordCountMain _program;
        private string[] _text = [];

        
        public string Word = "он";

        [GlobalSetup]
        public void Setup()
        {
            _program = new WordCountMain();
            var filePath = Path.Combine("..", "..", "..", "..", "..", "..", "..", "..", "text.txt");
            var textList = new List<string>();
            using var reader = new StreamReader(filePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
                textList.Add(line);
            _text = textList.ToArray();
        }

        [Benchmark(Baseline = true)]
        public long IndexOf() => WordCount_IndexOf(_text, Word);

        [Benchmark]
        public long Linq() => _program.WordCount_Linq(_text, Word);

        [Benchmark]
        public long Binary() => _program.WordCount_Binary(_text, Word);

        [Benchmark]
        public long Regex() => _program.WordCount_Regex(_text, Word);

     
    }

}