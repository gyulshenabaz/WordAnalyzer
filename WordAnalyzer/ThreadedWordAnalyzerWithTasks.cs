using System.Threading.Tasks;

namespace WordAnalyzer
{
    /// <summary>
    /// This class is used for analyzing words in a book using tasks
    /// </summary>
    public class ThreadedWorkAnalyzerWithTasks : WordAnalyzer
    {
        public ThreadedWorkAnalyzerWithTasks(Book book) : base(book) { }
        
        public override void Analyze()
        {
            _totalWordCount = Task.Run(() => this.GetCountOfAllWords()).Result;
            _longestWord = Task.Run(() => this.GetLongestWord()).Result;
            _shortestWord = Task.Run(() => this.GetShortestWord()).Result;
            _averageWordLength = Task.Run(() =>this.GetAverageWordLength()).Result;
            _mostCommonWords = Task.Run(() =>this.GetFiveMostCommonWords()).Result;
            _leastCommonWords = Task.Run(() =>this.GetFiveLeastCommonWords()).Result;
        }
    }
}