using System.Threading.Tasks;
using DataModel.Storages.Word;
using DataModel.Storages.Word.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uploader.Services;

namespace Uploader.Tests
{
    /// <summary>
    /// Класс для проверки методов класса WordService.
    /// </summary>
    [TestClass]
    public class WordServiceTests
    {
        private readonly WordService _service;
        private readonly Mock<IWordRepository> _wordRepositoryMock = new();

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public WordServiceTests()
        {
            _service = new WordService(_wordRepositoryMock.Object);
        }

        /// <summary>
        /// Проверка того, что метод добавит новый объект в БД.
        /// </summary>
        [TestMethod]
        public async Task UpdateDbAsync_ShouldAddNewItemToDb()
        {
            // Arrange
            var word = new WordModel("word", 1);
            _wordRepositoryMock.Setup(repository => repository.GetItemAsync(word.Word))
                .ReturnsAsync(word);

            // Act
            await _service.UpdateDbAsync(word);
            var newWord = await _wordRepositoryMock.Object.GetItemAsync("word");

            // Assert
            Assert.AreEqual(word.Word, newWord.Word);
            Assert.AreEqual(word.Count, newWord.Count);
        }

        /// <summary>
        /// Проверка того, что метод обновит количество упоминаний у уже существующего в БД слова.
        /// </summary>
        [TestMethod]
        public async Task UpdateDbAsync_ShouldUpdateExistingItem()
        {
            // Arrange
            var word = new WordModel("word", 1);
            var update = new WordModel("word", 4);

            _wordRepositoryMock.Setup(repository => repository.GetItemAsync(word.Word))
                .ReturnsAsync(word);
            _wordRepositoryMock.Setup(repository => repository.Update(It.IsAny<WordModel>(), It.IsAny<int>()))
                .Callback((WordModel wordModel, int count) =>
                {
                    wordModel.Count += count;
                });
            await _wordRepositoryMock.Object.CreateAsync(word);

            // Act
            await _service.UpdateDbAsync(update);
            var newWord = await _wordRepositoryMock.Object.GetItemAsync("word");

            // Assert
            Assert.AreEqual(newWord.Count, word.Count);
        }
    }
}
