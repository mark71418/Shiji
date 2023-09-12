using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderTask
{
    internal interface IWordFinder
    {
        IList<string> Find(IEnumerable<string> src);
    }

    internal class WordFinder : IWordFinder
    {
        private HashSet<string> _dictionary = new();

        private const int DICTIONARY_SIZE = 2048, MATRIX_MAX_ROW = 64, MATRIX_MAX_COL = 64;

        public WordFinder(IEnumerable<string> dictionary)
        {
            if (dictionary == null || dictionary.Count() == 0)
                throw new ArgumentNullException("Dictionary is empty");

            if (dictionary.Count() > DICTIONARY_SIZE)
                throw new ArgumentNullException($"Disctionary has exceeded the limit of {DICTIONARY_SIZE}");

            _dictionary = dictionary.ToHashSet();
        }

        public IList<string> Find(IEnumerable<string> src)
        {
            if (src == null || src.Count() == 0)
                throw new ArgumentNullException("Matrix cannot be empty");

            if (src.Count() > MATRIX_MAX_ROW)
                throw new ArgumentNullException($"Word matrix has exceeded {MATRIX_MAX_ROW} rows");

            if (src.Max(s => s.Length) > MATRIX_MAX_COL)
                throw new ArgumentNullException($"Word matrix has exceeded {MATRIX_MAX_ROW} columns");

            if (src.Sum(s => s.Length) != (src.Count() * src.First().Length))
                throw new ArgumentNullException("Number of matrix rows does not match with number of columns");

            var foundWords = new HashSet<string>();
            var rowCount = src.Count();
            var matrix = src.ToArray();

            foreach (var word in _dictionary)
            {
                var wordCount = word.Length;

                for (int matrixRow = 0; matrixRow < rowCount; matrixRow++)
                {
                    var matrixCharCount = matrix[matrixRow].Length;

                    for (int matrixColumn = 0; matrixColumn < matrixCharCount; matrixColumn++)
                    {
                        if (matrix[matrixRow][matrixColumn] == word[0])
                        {
                            if (InHorizontal(matrix[matrixRow], matrixColumn, word))
                            {
                                foundWords.Add(word);
                            }

                            if (InVertical(matrix, matrixColumn, word))
                            {
                                foundWords.Add(word);
                            }
                        }
                    }
                }
            }

            return foundWords.ToList();
        }

        private bool InHorizontal(string matrix, int matrixIndex, string word)
        {
            var matrixWord = GetHorizontalWord(matrix, matrixIndex, word.Length);

            if (matrixWord == null)
                return false;

            return string.Equals(matrixWord, word, StringComparison.OrdinalIgnoreCase);
        }

        private string? GetHorizontalWord(string matrix, int indexStart, int wordCount)
        {
            var matrixCount = matrix.Length;

            if ((matrixCount - indexStart) < wordCount)
                return null;

            return matrix.Substring(indexStart, wordCount);
        }

        private bool InVertical(IEnumerable<string> matrix, int matrixIndex, string word)
        {
            var matrixWord = GetVerticalWord(matrix, matrixIndex);

            if (matrixWord == null)
                return false;

            return matrixWord.Contains(word);
        }

        private string? GetVerticalWord(IEnumerable<string> words, int index)
        {
            return new String(words.Select(s => s[index])?.ToArray());
        }
    }
}
