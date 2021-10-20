using System.IO;
using System.Text;

namespace Uploader.PresentationLayer
{
    /// <summary>
    /// Интерфейс менеджера файлов для возможности тестирования без риска навредить файловой системе.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Метод, инициализирующий объект StreamReader
        /// </summary>
        /// <param name="path"> Путь до входного файла </param>
        /// <param name="encoding"> Кодировка чтения </param>
        /// <returns> Объект класса StreamReader </returns>
        StreamReader StreamReader(string path, Encoding encoding);
    }
}