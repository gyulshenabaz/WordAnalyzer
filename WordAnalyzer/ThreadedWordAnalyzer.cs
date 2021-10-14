using System.Threading;

namespace WordAnalyzer
{
    /// <summary>
    /// This class is used for analyzing words in a book using threads
    /// </summary>
    public class ThreadedWordAnalyzer : WordAnalyzer
    {
        public ThreadedWordAnalyzer(Book book) : base(book) { }
        
        public override void Analyze()
        {
            Thread[] threads =
            {
                new Thread(() => _totalWordCount = this.GetCountOfAllWords()),
                new Thread(() => _longestWord = this.GetLongestWord()),
                new Thread(() => _shortestWord = this.GetShortestWord()),
                new Thread(() => _averageWordLength = this.GetAverageWordLength()),
                new Thread(() => _mostCommonWords = this.GetFiveMostCommonWords()),
                new Thread(() => _leastCommonWords = this.GetFiveLeastCommonWords()),
            };

            foreach (var thread in threads)
            {
                thread.Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i].ThreadState != ThreadState.Unstarted)
                {
                    threads[i].Join();
                }
            }
        }
    }
}