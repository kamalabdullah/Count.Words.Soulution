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
    public class CountWordsService : ICountWordsService
    {
        public IDictionary<string, int> CountWords(string dirPath)
        {
            string[] files = Directory.GetFiles(dirPath, "*.txt");

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string file in files)
            {
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
                                if (wordCount.ContainsKey(word))
                                    wordCount[word]++;
                                else
                                    wordCount[word] = 1;
                            }
                        }
                    }
                }

            }
            return wordCount;
        }
    }
}
