﻿using Application.Interfaces;
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
        private string _dirPath;
        public CountWordsService(string dirPath)
        {
            _dirPath = dirPath;
        }
        public IDictionary<string, int> CountWords()
        {
            string[] files = Directory.GetFiles(_dirPath, "*.txt");

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string file in files)
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
