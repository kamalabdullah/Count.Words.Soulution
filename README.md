# Count Words Application

This is a C# application for counts the occurrences of each unique word in the files for specific directory folder and aggregate the results.

## Installation

To use this project, you can clone the reposatory using git command:

```bash
$ git clone https://github.com/kamalabdullah/Count.Words.Soulution.git
```

## Demo

To run the application run the cmd prompot in {your local path}\Count.Words.Soulution\Count.Words.App, then run this cmd command:

```bash
dotnet run
```
Then enter a valid folder path that contains *.text files that you need to count all unique words you will see for each word  as bellow with format
{Word Occurrences Count}:{Word}

![image](https://github.com/kamalabdullah/Count.Words.Soulution/assets/19591676/d46e52d2-265c-40f6-822e-7e007a8193b3)

## Methods

The **CountWordsServiceFactory** class provides the following methods:

  `GetCountWordsService(string dirPath, AppSettings settings)`
  
GetCountWordsService is a method that responsible for returns even Counts Word Service or Parallel Count Word Service based on files sizes and files count in the directory folder and those numbers are configured in appsettings file.

Parameters:

* `settings` (AppSettings): the object of settings variables in application.
* `dirPath` (string): the folder path that contains text files.

Returns:
It will return an ony object from classs that implement ICountWordsService, in our case here will be one of the following services.

- CountWordsService.

     OR
 
- ParallelCountWordsService.


The **CountWordsService** class provides the following methods:

  `CountWords()`

CountWords is the main method for our application that is responsible for counting occurrences of each unique word and this method is implemented with and run in the main thread for the application.

Returns:
- IDictionary<string, int> for each word and occurrences count.


The **ParallelCountWordsService** class provides the following methods:

  `CountWords()`

CountWords is the main method for our application that is responsible for counting occurrences of each unique word and this method is implemented with parallelism that will create a thread for each file to process it.

Returns:
- IDictionary<string, int> for each word and occurrences count.

    **Note: the returned dictionary here will not be ordered with the same word order in the files as each thread will be responsible to handle separate file**

## Settings Variables
To run this project, you will need to setup the following settings variables to your appsettings.json file

- SizeForFilesToUseParallelism: This variable is one of two parameters that if the files size in the directory is greater than this number will use the Parallel implementation and this variable in MegaBytes

- FilesCountToUseParallelism: This variable is one of two parameters that if the files count in the directory greater than this number will use the Parallel implementation.


## Appendix

This application implemented with two moods 

1- Basic implementation for read and processing the text files. 

2- threading or parallelism implementation for read and processing the text files.

and each mood of them has value of fit in spesific case:-

**case 1**: The number and the size of files are small like 1, 2, or 4  files with small or medium sizes, so in this case, the basic implementation will be more fitting, as the creation and handling of the threads and locking the dictionary while inserting value on it will cost more time than just if we run this files sequentially.

**case 2**: The number and the size of files are big, so in this case, the threads or parallel implementation will be more fitting to handle more file in the same time.

**The choice between the two moods based on file size and file count is decided based on testing, as there is no recommended number for using this**.

I made many runs for performing unit testing on small and large files (500 MegaBytes for each file) and found that in our case here the parallelism solution will be more affectionate on large files and in files will be 5 or more.

Result of unit testing with one lagre file (500 MegaBytes) in Milliseconds
![First Run](https://github.com/kamalabdullah/Count.Words.Soulution/assets/19591676/84f0d199-07f8-4408-b185-b142b5a16980)


Result of unit testing with 5 lagre file (500 MegaBytes for each file) in Milliseconds
![Fifth Run](https://github.com/kamalabdullah/Count.Words.Soulution/assets/19591676/d4025e10-5ff4-4b61-89fa-af44aa21adf7)


## License
This library is released under the [MIT](https://choosealicense.com/licenses/mit/)  License.
