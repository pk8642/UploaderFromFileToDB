using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Storages.Word.Models
{
    /// <summary>
    /// Класс сущности слова.
    /// </summary>
    [Index(nameof(Word))]
    public class WordModel
    {
        /// <summary>
        /// Слово, упоминающееся в различных текстах.
        /// </summary>
        [Key]
        public string Word { get; set; }
        /// <summary>
        /// Количество упоминаний данного слова.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Конструктор модели, создает объект класса, который будет добавляться или обновляться в БД.
        /// </summary>
        /// <param name="word"> Некоторое упоминающееся в текстах слово. </param>
        /// <param name="count"> Количество упоминаний данного слова. </param>
        public WordModel(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}