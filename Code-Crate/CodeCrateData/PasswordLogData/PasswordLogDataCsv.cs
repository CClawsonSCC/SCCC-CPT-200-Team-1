using CsvHelper;
using System.Globalization;
namespace CodeCrateData;

public class PasswordLogDataCsv
{
    String userAccountCsvFilePath = "CodeCrateData/PasswordLogData/PasswordLog.csv";
    public async Task<IEnumerable<PasswordLog>> LoadCollection()
    {
        using (var reader = new StreamReader(userAccountCsvFilePath))
        using (var csvRead = new CsvReader(reader, CultureInfo.InvariantCulture))
        {   
            return await csvRead.GetRecordsAsync<PasswordLog>().ToListAsync<PasswordLog>();
        }
    }

    public async Task WriteCollection(IEnumerable<PasswordLog> passwordLogs)
    {
        using (var writer = new StreamWriter(userAccountCsvFilePath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(passwordLogs);
        }
    }
}