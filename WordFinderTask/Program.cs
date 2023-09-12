namespace WordFinderTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] matrixStrings = { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            string[] wordsToFind = { "wind", "cold", "chill", "chill" };
            IWordFinder wordFinder = new WordFinder(wordsToFind);

            var words = wordFinder.Find(matrixStrings);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.Read();
        }
    }
}