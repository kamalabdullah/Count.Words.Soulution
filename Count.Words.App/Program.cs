
using Application.Interfaces;
using Application.Services;
using System.Diagnostics;

class Program
{
    static void Main()
    {

        ICountWordsService countWordsService = new CountWordsService();
        IValidateService validateService = new ValidateService();

        Console.WriteLine("Please enter the directory path for files");
        string dirPath = Console.ReadLine();
        while (!validateService.ValidatePath(dirPath))
        {
            Console.WriteLine("Invalid path please enter a valid path");
            dirPath = Console.ReadLine();
        }
        IDictionary<string, int> wordsCount = countWordsService.CountWords(dirPath);
        foreach (var wordItem in wordsCount)
        {
            Console.WriteLine(wordItem.Value + ":" + wordItem.Key);
        }
    }
}