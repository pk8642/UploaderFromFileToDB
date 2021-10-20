using System.IO;
using System.Text;

namespace Uploader.PresentationLayer
{
    /// <summary>
    /// Класс обработчика файлов
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Метод, инициализирующий объект StreamReader
        /// </summary>
        /// <param name="path"> Путь до входного файла </param>
        /// <param name="encoding"> Кодировка чтения </param>
        /// <returns> Объект класса StreamReader </returns>
        public StreamReader StreamReader(string path, Encoding encoding)
        {
            return new StreamReader(path, encoding);
        }
    }
}