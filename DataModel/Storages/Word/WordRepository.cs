using System;
using System.Threading.Tasks;
using DataModel.Contexts;
using DataModel.Storages.Word.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataModel.Storages.Word
{
    /// <summary>
    /// Класс репозитория модели класса WordModel, реализующий методы интерфейса IRepository. 
    /// </summary>
    public class WordRepository : IWordRepository
    {
        private readonly ApplicationContext _db;
        private bool _disposed;

        /// <summary>
        /// Конструктор данного класса.
        /// Использует переданный контекст данных для совершения операций с БД.
        /// </summary>
        /// <param name="context"> Контекст данных. </param>
        public WordRepository(ApplicationContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Метод, получающий слово из БД по переданному ключу.
        /// </summary>
        /// <param name="word"> Слово, которое требуется найти в БД. </param>
        public async Task<WordModel> GetItemAsync(string word)
        {
            return await _db.Words.FindAsync(word);
        }

        /// <summary>
        /// Метод, добавляющий объект модели WordModel в БД.
        /// </summary>
        /// <param name="item"> Объект модели WordModel </param>
        public async Task<EntityEntry<WordModel>> CreateAsync(WordModel item)
        {
            return await _db.Words.AddAsync(item);
        }

        /// <summary>
        /// Метод, обновляющий количество упоминаний слова в БД.
        /// </summary>
        /// <param name="item"> Объект модели WordModel, который требуется обновить. </param>
        /// <param name="count"> Количество упоминаний в новом тексте. </param>
        public void Update(WordModel item, int count)
        {
            item.Count += count;
        }

        /// <summary>
        /// Метод, сохраняющий изменения в БД.
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }


        /// <summary>
        /// Освобождает неиспользующиеся ресурсы
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Метод, освобождающий неиспользующиеся ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
