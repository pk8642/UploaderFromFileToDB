using System.Collections.Generic;
using System.Threading.Tasks;
using Uploader.Configuration;
using Uploader.PresentationLayer;

namespace Uploader.Logic
{
    /// <summary>
    /// Класс для обработки команды записи в БД.
    /// </summary>
    public class TransferHandler
    {
        /// <summary>
        /// Метод, реализующий перенос часто упоминаемых слов в БД.
        /// </summary>
        public async Task TransferToDbAsync(string filePath)
        {
            var wordsDictionary = new Dictionary<string, int>();
            IFileManager streamReader = new FileManager();
            var parser = new Parser(streamReader);
            parser.ParseFile(filePath, wordsDictionary);
            parser.RemoveRareWords(wordsDictionary);

            await UploadFromDictionaryToDbAsync(wordsDictionary);
        }

        private async Task UploadFromDictionaryToDbAsync(Dictionary<string, int> wordsDictionary)
        {
            if (wordsDictionary.Count > 0)
            {
                var logic = Configurator.Configure();
                foreach (var (word, count) in wordsDictionary)
                {
                    await logic.UpdateDbAsync(word, count);
                }
            }
        }
    }
}