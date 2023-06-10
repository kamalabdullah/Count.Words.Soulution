using Application.Interfaces;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    public class PerformanceTests
    {
        private ICountWordsService countWordsService;

        [Fact]
        public void CountWords_PerformanceTestSmallFiles()
        {
            // Arrange
            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Assets\\Correct Folder");
            countWordsService = new CountWordsService(directoryPath);

            // Act
            var timerBasic = Stopwatch.StartNew();

            var basicWordCounter = countWordsService.CountWords();

            timerBasic.Stop();

            Debug.WriteLine("Basic timer: " + timerBasic.ElapsedMilliseconds);

            ICountWordsService parallelCountWordsService = new ParallelCountWordsService(directoryPath);

            var timerParallel = Stopwatch.StartNew();

            var ParallelWordCounter = parallelCountWordsService.CountWords();

            timerParallel.Stop();
            Debug.WriteLine("Parallel time: " + timerParallel.ElapsedMilliseconds);

            // Assert
            Assert.True(timerBasic.ElapsedMilliseconds < timerParallel.ElapsedMilliseconds);
            Assert.Equal(basicWordCounter, ParallelWordCounter);
        }

        [Fact]
        public void CountWords_PerformanceTestLargFiles()
        {
            // Arrange
            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Assets\\Performance");
            countWordsService = new CountWordsService(directoryPath);
            int numberOfFiles = 6;
            GenerateLargFileTest(directoryPath, numberOfFiles);
            ICountWordsService parallelCountWordsService = new ParallelCountWordsService(directoryPath);

            // Act
            var timerParallel = Stopwatch.StartNew();
            var ParallelWordCounter = parallelCountWordsService.CountWords();
            timerParallel.Stop();
            Debug.WriteLine("Parallel time for {0} larg Files: " + timerParallel.ElapsedMilliseconds, numberOfFiles);


            var timerBasic = Stopwatch.StartNew();
            var basicWordCounter = countWordsService.CountWords();
            timerBasic.Stop();

            Debug.WriteLine("Basic timer for {0} larg Files:  " + timerBasic.ElapsedMilliseconds, numberOfFiles);

            // Assert
            Assert.True(timerBasic.ElapsedMilliseconds > timerParallel.ElapsedMilliseconds);
        }

        private void GenerateLargFileTest(string path,int filesNumber)
        {
            for (int i = 0; i < filesNumber; i++)
            {
                string filePath = path + String.Format("\\huge{0}.txt", i);
                long fileSizeBytes = 500 * 1024 * 1024; // 500 megabytes

                if (!File.Exists(filePath))
                {
                    GenerateLargeTextFile(filePath, fileSizeBytes);
                }
            }

        }

        private void GenerateLargeTextFile(string filePath, long fileSizeBytes)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                long remainingBytes = fileSizeBytes;

                while (remainingBytes > 0)
                {
                    string chunk = GenerateChunkOfText(remainingBytes < int.MaxValue ? (int)remainingBytes : int.MaxValue);

                    writer.Write(chunk);

                    remainingBytes -= chunk.Length;
                }
            }
        }

        private string GenerateChunkOfText(int chunkSize)
        {
            // Define the characters to use for generating text
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

            Random random = new Random();

            // Generate a random chunk of text with the specified size
            return new string(Enumerable.Repeat(characters, chunkSize)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
