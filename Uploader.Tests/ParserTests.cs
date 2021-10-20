using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uploader.Logic;
using Uploader.PresentationLayer;

namespace Uploader.Tests
{
    /// <summary>
    /// Класс для тестирования методов класса Parser
    /// </summary>
    [TestClass]
    public class ParserTests
    {
        /// <summary>
        /// Проверка того, что слова из абстрактного текста добавятся в словарь.
        /// </summary>
        [TestMethod]
        public void ParseFile_ShouldAddWordsToDictionary()
        {
            // Arrange
            var dictionary = new Dictionary<string, int>();
            var mockFileManager = new Mock<IFileManager>();
            const string fakeFileContent = "word word word word";
            var fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContent);
            var parser = new Parser(mockFileManager.Object);

            var fakeMemoryStream = new MemoryStream(fakeFileBytes);

            mockFileManager.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>(), Encoding.UTF8))
                .Returns(() => new StreamReader(fakeMemoryStream));

            // Act
            parser.ParseFile(fakeFileContent, dictionary);

            // Assert
            Assert.AreEqual(true, dictionary.ContainsKey("word"));
            Assert.AreEqual(4, dictionary["word"]);
        }

        /// <summary>
        /// Проверка того, что неподходящие по условию слова будут удалены из словаря
        /// </summary>
        [TestMethod]
        public void RemoveRareWords_ShouldRemove()
        {
            // Arrange
            var dictionary = new Dictionary<string, int>
            {
                { "word", 4 },
                { "wordToDelete", 3 }
            };
            var mockFileManager = new Mock<IFileManager>();
            var parser = new Parser(mockFileManager.Object);

            // Act
            parser.RemoveRareWords(dictionary);

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual("word", dictionary.Keys.First());
            Assert.ThrowsException<KeyNotFoundException>(() => dictionary["wordToDelete"]);
        }
    }
}
