﻿using Application.Interfaces;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    public class CountWordsServiceTests
    {
        private ICountWordsService countWordsService;

        [Fact]
        public void CountWords_ShouldReturnCorrectWordCount()
        {
            // Arrange
            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Assets\\Correct Folder");
            countWordsService = new CountWordsService(directoryPath);

            // Act
            var wordCount = countWordsService.CountWords();

            // Assert
            Assert.Equal(1, wordCount["Go"]);
            Assert.Equal(2, wordCount["do"]);
            Assert.Equal(2, wordCount["that"]);
            Assert.Equal(1, wordCount["thing"]);
            Assert.Equal(1, wordCount["you"]);
            Assert.Equal(1, wordCount["so"]);
            Assert.Equal(2, wordCount["well"]);
            Assert.Equal(1, wordCount["I"]);
            Assert.Equal(1, wordCount["play"]);
            Assert.Equal(1, wordCount["football"]);
        }

        [Fact]
        public void CountWords_ShouldHandleFilesWithNoWords()
        {
            // Arrange
            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Assets\\Empty File");
            countWordsService = new CountWordsService(directoryPath);

            // Act
            var wordCount = countWordsService.CountWords();

            // Assert
            Assert.Empty(wordCount);
        }

        [Fact]
        public void CountWords_ShouldHandleEmptyDirectory()
        {
            // Arrange
            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Assets\\Empty Folder");
            countWordsService = new CountWordsService(directoryPath);

            // Act
            var wordCount = countWordsService.CountWords();

            // Assert
            Assert.Empty(wordCount);
        }

    }
}
