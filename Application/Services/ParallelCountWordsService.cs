using Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ParallelCountWordsService: ICountWordsService
    {

        private string _dirPath;
        public ParallelCountWordsService(string dirPath)
        {
            _dirPath = dirPath;
        }
        public IDictionary<string, int> CountWords()
        {
            string[] files = Directory.GetFiles(_dirPath, "*.txt");

            ConcurrentDictionary<string, int> wordCount = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(files, file =>
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');

                        foreach (string word in words)
                        {
                            if (!string.IsNullOrWhiteSpace(word))
                            {
                                wordCount.AddOrUpdate(word, 1, (_, count) => count + 1);
                            }
                        }
                    }
                }
            });

            return wordCount;
        }
    }
}
