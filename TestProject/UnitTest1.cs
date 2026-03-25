using WordCount;

using static WordCount.WordCountMain;

namespace TestProject
{
    public class Tests
    {
        private static readonly string He = "он";
        private static readonly string She = "она";
        private static readonly string They = "они";

        WordCount.WordCountMain _program;
        string[] _text;
        [SetUp]
        public void Setup()
        {
            _program = new WordCountMain();

            var filePath = Path.Combine("..", "..", "..", "..", "text.txt");


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
            _text = textList.ToArray();
        }

        [Test]
        public void Test_He()
        {
            var expected = WordCount_IndexOf(_text, He);
            Assert.Multiple(() =>
            {
                Assert.That(_program.WordCount_Linq(_text, He), Is.EqualTo(expected), "Linq");
                Assert.That(_program.WordCount_Binary(_text, He), Is.EqualTo(expected), "Binary");
                Assert.That(_program.WordCount_Regex(_text, He), Is.EqualTo(expected), "Regex");
               
            });
        }
        [Test]
        public void Test_She()
        {
            var expected = WordCount_IndexOf(_text, She);
            Assert.Multiple(() =>
            {
                Assert.That(_program.WordCount_Linq(_text, She), Is.EqualTo(expected), "Linq");
                Assert.That(_program.WordCount_Binary(_text, She), Is.EqualTo(expected), "Binary");
                Assert.That(_program.WordCount_Regex(_text, She), Is.EqualTo(expected), "Regex");
              
            });
        }
        [Test]
        public void Test_They()
        {
            var expected = WordCount_IndexOf(_text, They);
            Assert.Multiple(() =>
            {
                Assert.That(_program.WordCount_Linq(_text, They), Is.EqualTo(expected), "Linq");
                Assert.That(_program.WordCount_Binary(_text, They), Is.EqualTo(expected), "Binary");
                Assert.That(_program.WordCount_Regex(_text, They), Is.EqualTo(expected), "Regex");
               
            });
        }

    }
}
