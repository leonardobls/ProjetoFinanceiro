

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace BuscadorFinanceiro.Controllers
{
    public class FileReaderController
    {

        public static List<dynamic> GetContent(String path)
        {
            using var reader = new StreamReader(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                Delimiter = ";",
                IgnoreBlankLines = true,

            };

            var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<dynamic>().ToList();
            Console.WriteLine(records);

            return records;
        }
    }
}