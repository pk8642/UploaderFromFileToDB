using System.IO;
using DataModel.Contexts;
using DataModel.Storages.Word;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Uploader.Services;

namespace Uploader.Configuration
{
    /// <summary>
    /// Класс для подключения к базе данных.
    /// </summary>
    public class Configurator
    {
        /// <summary>
        /// Метод выполняет подключение к базе данных MSSQL Server
        /// </summary>
        /// <returns> Сервис для обращения к БД и проведения необходимых операций </returns>
        public static WordService Configure()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connStr = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connStr)
                .Options;

            return new WordService(new WordRepository(new ApplicationContext(options)));
        }
    }
}