using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Uploader.PresentationLayer
{
    /// <summary>
    /// Класс для обработки входных данных.
    /// </summary>
    public class Parser
    {
        private readonly IFileManager _fileManager;

        /// <summary>
        /// Конструктор класса Parser
        /// </summary>
        /// <param name="fileManager"> Менеджер файлов </param>
        public Parser(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// Метод считывает текст из входного файла по переданному пути и преобразует его
        /// в словарь для более удобной загрузки в БД.
        /// </summary>
        /// <param name="path"> Путь до входного файла </param>
        /// <param name="wordsDictionary"> Словарь, содержащий упоминающиеся слова и количество упоминаний. </param>
        public void ParseFile(string path, Dictionary<string, int> wordsDictionary)
        {
            try
            {
                using var streamReader = _fileManager.StreamReader(path, System.Text.Encoding.UTF8);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    AddWordsToDictWithCount(line, wordsDictionary);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
            }
        }

        private static void AddWordsToDictWithCount(string line, Dictionary<string, int> wordsDictionary)
        {
            foreach (var e in line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries))
            {
                var word = Regex.Replace(e, "\\p{P}+", "").ToLower();
                if (IsWordAcceptable(word))
                {
                    if (wordsDictionary.ContainsKey(word))
                    {
                        wordsDictionary[word]++;
                    }
                    else
                    {
                        wordsDictionary.Add(word, 1);
                    }
                }
            }
        }
        private static bool IsWordAcceptable(string word)
        {
            return word.Length is >= 3 and <= 20;
        }

        /// <summary>
        /// Метод убирает из итогового словаря те позиции, которые не подходят
        /// по ТЗ (в данном случае, слова, которые встречались менее, чем 4 раза).
        /// </summary>
        /// <param name="dict"> Словарь, содержащий упоминающиеся слова и количество упоминаний. </param>
        public void RemoveRareWords(Dictionary<string, int> dict)
        {
            foreach (var item in dict.Where(pair => pair.Value < 4).ToList())
            {
                dict.Remove(item.Key);
            }
        }
    }
}
