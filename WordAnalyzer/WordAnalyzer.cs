using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAnalyzer
{
    /// <summary>
    /// This class is used for analyzing words in a book in sequential manner
    /// </summary>
    public class WordAnalyzer
    {
        private string[] _allWords;
        protected int _totalWordCount;
        protected string _shortestWord;
        protected string _longestWord;
        protected double _averageWordLength;
        protected string[] _mostCommonWords;
        protected string[] _leastCommonWords;
        
        public int TotalWordCount => _totalWordCount;
        public string ShortestWord => _shortestWord;
        public string LongestWord => _longestWord;
        public double AverageWordLength => _averageWordLength;
        public string[] MostCommonWords => _mostCommonWords;
        public string[] LeastCommonWords => _leastCommonWords;
        
        public WordAnalyzer(Book book)
        {
            this._allWords = new BookReader().GetAllWords(book.Text);
        }
        
        /// <summary>
        /// Analyze a book. This method performs various operations to analyze all words from a book
        /// </summary>
        public virtual void Analyze()
        {
            _totalWordCount = this.GetCountOfAllWords();
            _shortestWord = this.GetShortestWord();
            _longestWord = this.GetLongestWord();
            _averageWordLength = this.GetAverageWordLength();
            _mostCommonWords = this.GetFiveMostCommonWords();
            _leastCommonWords = this.GetFiveLeastCommonWords();
        }
        
        /// <summary>
        /// Returns number of all words
        /// </summary>
        protected int GetCountOfAllWords()
        {
            return _allWords.Length;
        }

        /// <summary>
        /// Returns the shortest word
        /// </summary>
        protected string GetShortestWord()
        {
            int minIndex = 0;
            for (int i = 0; i < _allWords.Length; i++)
            {
                if (_allWords[minIndex].Length > _allWords[i].Length)
                {
                    minIndex = i;
                }
            }

            return _allWords[minIndex];
        }

        /// <summary>
        /// Returns the longest word
        /// </summary>
        protected string GetLongestWord()
        {
            int maxIndex = 0;
            for (int i = 0; i < _allWords.Length; i++)
            {
                if (_allWords[maxIndex].Length < _allWords[i].Length)
                {
                    maxIndex = i;
                }
            }

            return _allWords[maxIndex];
        }
        
        /// <summary>
        /// Returns the average word length
        /// </summary>
        protected double GetAverageWordLength()
        {
            return  (double)_allWords.Sum(w => w.Length) / _allWords.Length;
        }
        
        /// <summary>
        /// Returns the five most common words
        /// </summary>
        protected string[] GetFiveMostCommonWords()
        {
            var map = this.GetAllWordOccurrences();

            var fiveMostCommonWords = map
                .OrderByDescending(m => m.Value)
                .Select(m => m.Key)
                .Take(5)
                .ToArray();

            return fiveMostCommonWords;
        }
        
        /// <summary>
        /// Returns the five least common words
        /// </summary>
        protected string[] GetFiveLeastCommonWords()
        {
            var map = this.GetAllWordOccurrences();

            var fiveLeastCommonWords = map
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .Take(5)
                .ToArray();

            return fiveLeastCommonWords;
        }
        
        /// <summary>
        /// Override to capture the findings from the analysis. 
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($@"Total number of all words in book: {TotalWordCount}");
            sb.AppendLine($@"Shortest word: {ShortestWord}");
            sb.AppendLine($@"Longest word: {LongestWord}");
            sb.AppendLine($@"Average word length: {AverageWordLength:f2}");
            sb.AppendLine($@"Five most common words: {String.Join(", ", MostCommonWords)}");
            sb.AppendLine($@"Five least common words: {String.Join(", ", LeastCommonWords)}");

            return sb.ToString();
        }

        /// <summary>
        /// Returns number of all word occurrences
        /// </summary>
        private Dictionary<string, int> GetAllWordOccurrences()
        {
            var wordMap = new Dictionary<string, int>();

            foreach (var word in _allWords)
            {
                // Using invariant: https://stackoverflow.com/questions/3550213/in-c-sharp-what-is-the-difference-between-toupper-and-toupperinvariant
                var upperWord = word.ToUpperInvariant();
                
                if (!wordMap.ContainsKey(upperWord))
                {
                    wordMap[upperWord] = 1;
                }

                ++wordMap[upperWord];
            }

            return wordMap;
        }
    }
}