using DataModel.Storages.Word.Models;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Contexts
{
    /// <summary>
    /// Класс контекста данных.
    /// Содержит множество объектов, хранящееся в БД.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Множество слов, таблица в БД, содержащяя само слово и количесвто его упоминаний.
        /// </summary>
        public DbSet<WordModel> Words { get; set; }

        /// <summary>
        /// Метод выполняет проверку, что БД создана, если нет, то создает ее.
        /// </summary>
        /// <param name="options"> Свойства контекста данных. </param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}