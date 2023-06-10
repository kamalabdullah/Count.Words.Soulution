
using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        AppSettings appSettings = GetConfigrations();

        ICountWordsServiceFactory countWordsServiceFactory = new CountWordsServiceFactory();
        IValidateService validateService = new ValidateService();

        Console.WriteLine("Please enter the directory path for files");
        string dirPath = Console.ReadLine();
        while (!validateService.ValidatePath(dirPath))
        {
            Console.WriteLine("Invalid path please enter a valid path");
            dirPath = Console.ReadLine();
        }
        ICountWordsService countWordsService = countWordsServiceFactory.GetCountWordsService(dirPath,appSettings);
        IDictionary<string, int> wordsCount = countWordsService.CountWords();
        foreach (var wordItem in wordsCount)
        {
            Console.WriteLine(wordItem.Value + ":" + wordItem.Key);
        }
    }

    private static AppSettings GetConfigrations()
    {
        var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();
        AppSettings appSettings = config.GetSection("AppSettings").Get<AppSettings>();
        return appSettings;
    }
}