using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountWordsServiceFactory : ICountWordsServiceFactory
    {
        public ICountWordsService GetCountWordsService(string dirPath, AppSettings settings) {

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            var files = dirInfo.GetFiles("*.txt");
            var filesSize = dirInfo.GetFiles("*.txt").Sum(fi => fi.Length);
            long assumedSizeForFilesToUseParallelism = settings.SizeForFilesToUseParallelism != null? settings.SizeForFilesToUseParallelism : 524288000; // 500 MigaByte
            int assumedFilesCountToUseParallelism = settings.FilesCountToUseParallelism != null ? settings.FilesCountToUseParallelism : 5; 
            if (files.Count() > assumedFilesCountToUseParallelism && filesSize > assumedSizeForFilesToUseParallelism)
            {
                return new ParallelCountWordsService(dirPath);
            }
            return new CountWordsService(dirPath);
           
        }
    }
}
