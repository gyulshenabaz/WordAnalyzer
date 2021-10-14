using System;
using System.Diagnostics;
using System.IO;

namespace WordAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("../../../Books/Dimityr_Dimov_Tjutjun_1960.txt");

            Book book = new Book("Tютюн", "Димитър Талев", text);

            AnalyzeSequentially(book);
            AnalyzeWithThreads(book);
            AnalyzeThreadedWithTasks(book);
        }

        static void AnalyzeSequentially(Book book)
        {
            WordAnalyzer wordAnalyzer = new WordAnalyzer(book);
            
            Console.WriteLine($"Started analysis for {book.Title}: ");
            
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();
            
            wordAnalyzer.Analyze();

            stopWatch.Stop();
            
            Console.WriteLine($"Completed analysis for {book.Title} in {stopWatch.ElapsedMilliseconds / 1000f}s");
            Console.WriteLine(wordAnalyzer.ToString());
        }
        
        static void AnalyzeWithThreads(Book book)
        {
            ThreadedWordAnalyzer threadedWordAnalyzer = new ThreadedWordAnalyzer(book);
            
            Console.WriteLine($"Started threaded analysis for for {book.Title}: ");
            
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();
            
            threadedWordAnalyzer.Analyze();

            stopWatch.Stop();
            
            Console.WriteLine($"Completed analysis for {book.Title} in {stopWatch.ElapsedMilliseconds / 1000f}s");
            Console.WriteLine(threadedWordAnalyzer.ToString());
        }
        
        static void AnalyzeThreadedWithTasks(Book book)
        {
            Console.WriteLine($"Started threaded analysis for {book.Title} using tasks: ");
            
            ThreadedWorkAnalyzerWithTasks threadedBookAnalyzer = new ThreadedWorkAnalyzerWithTasks(book);
            
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();
            
            threadedBookAnalyzer.Analyze();

            stopWatch.Stop();
            
            Console.WriteLine($"Finished threaded analysis for {book.Title} in {stopWatch.ElapsedMilliseconds / 1000f}s");
            Console.WriteLine(threadedBookAnalyzer.ToString());
        }
    }
}