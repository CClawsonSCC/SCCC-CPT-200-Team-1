using CsvHelper;
using System.Globalization;
namespace CodeCrateData;

public class CodeCrateDataCsv
{
    public async Task<IEnumerable<T>> LoadCollection<T>(String filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csvRead = new CsvReader(reader, CultureInfo.InvariantCulture))
        {   
            return await csvRead.GetRecordsAsync<T>().ToListAsync<T>();
        }
    }

    public async Task WriteCollection<T>(IEnumerable<T> dataObject, String filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(dataObject);
        }
    }
}