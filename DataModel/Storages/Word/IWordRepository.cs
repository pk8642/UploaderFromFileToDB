using System;
using System.Threading.Tasks;
using DataModel.Storages.Word.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataModel.Storages.Word
{
    /// <summary>
    /// Интерфейс репозитория для возможности тестирования без необходимости подключения к БД.
    /// </summary>
    public interface IWordRepository : IDisposable
    {
        /// <summary>
        /// Метод, добавляющий объект модели WordModel в БД.
        /// </summary>
        /// <param name="item"> Объект модели WordModel </param>
        Task<EntityEntry<WordModel>> CreateAsync(WordModel item);

        /// <summary>
        /// Метод, обновляющий количество упоминаний слова в БД.
        /// </summary>
        /// <param name="item"> Объект модели WordModel, который требуется обновить. </param>
        /// <param name="count"> Количество упоминаний в новом тексте. </param>
        void Update(WordModel item, int count);

        /// <summary>
        /// Метод, сохраняющий изменения в БД.
        /// </summary>
        Task<int> SaveAsync();

        /// <summary>
        /// Метод, получающий слово из БД по переданному ключу.
        /// </summary>
        /// <param name="word"> Слово, которое требуется найти в БД. </param>
        Task<WordModel> GetItemAsync(string word);
    }
}