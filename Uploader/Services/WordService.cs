using System.Threading.Tasks;
using DataModel.Storages.Word;
using DataModel.Storages.Word.Models;

namespace Uploader.Services
{
    /// <summary>
    /// Класс сервиса, выполняющий необходимые операции с БД.
    /// </summary>
    public class WordService
    {
        private readonly IWordRepository _repository;

        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="repository"> Контекст данных, содержащий множества объектов, хранящихся в БД. </param>
        public WordService(IWordRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Метод для обновления данных в БД. Обновляет число, отвечающее за количество упоминаний данного
        /// слова в тексте, либо создает новую строку в БД, если данного слова в БД нет.
        /// </summary>
        /// <param name="word"> Некоторое упоминающееся в текстах слово. </param>
        /// <param name="count"> Количество упоминаний данного слова. </param>
        public async Task UpdateDbAsync(string word, int count)
        {
            var item = _repository.GetItemAsync(word).Result;
            if (item == null)
            {
                await _repository.CreateAsync(new WordModel(word, count));
            }
            else
            {
                _repository.Update(item, count);
            }

            await _repository.SaveAsync();
        }

        /// <summary>
        /// Перегрузка метода UpdateDbAsync, позволяющая вызывать метод с аргументом объекта WordModel.
        /// </summary>
        /// <param name="word"> Объект класса WordModel. </param>
        public async Task UpdateDbAsync(WordModel word)
        {
            await UpdateDbAsync(word.Word, word.Count);
        }
    }
}