using System.Threading.Tasks;
using Uploader.Logic;

namespace Uploader
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Основной метод программы, запускает само приложение.
        /// </summary>
        /// <param name="args"> Аргументы, переданные с запуском приложения из консоли. </param>
        public static async Task Main(string[] args)
        {
            var filePath = args[0];
            var transferHandler = new TransferHandler();
            await transferHandler.TransferToDbAsync(filePath);
        }
    }
}