using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace CodeCrateData;

public class CodeCrateDataCsv
{
    
    public async Task<IEnumerable<T>> LoadCollection<T>(String filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false, // Removes the headers on all csv files
        };
        using (var reader = new StreamReader(filePath))
        using (var csvRead = new CsvReader(reader, config))
        {   
            return await csvRead.GetRecordsAsync<T>().ToListAsync<T>();
        }
    }

    public async Task WriteCollection<T>(IEnumerable<T> dataObject, String filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false, // Removes the headers on all csv files
        };
        //using (var writer = new StreamWriter(filePath)) // previous line
        using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.Unicode)) //cipher compatible line
        using (var csvWriter = new CsvWriter(writer, config))
        
        {
            await csvWriter.WriteRecordsAsync(dataObject);
        }
    }
}