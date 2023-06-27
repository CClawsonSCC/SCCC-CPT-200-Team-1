using CsvHelper;
using System.Globalization;
namespace CodeCrateData;

public class UserAccountDataCsv
{
    String userAccountCsvFilePath = "CodeCrateData/UseraccountData/UserAccount.csv";
    public async Task<IEnumerable<UserAccount>> LoadCollection()
    {
        using (var reader = new StreamReader(userAccountCsvFilePath))
        using (var csvRead = new CsvReader(reader, CultureInfo.InvariantCulture))
        {   
            return await csvRead.GetRecordsAsync<UserAccount>().ToListAsync<UserAccount>();
        }
    }

    public async Task WriteCollection(IEnumerable<UserAccount> userAccounts)
    {
        using (var writer = new StreamWriter(userAccountCsvFilePath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(userAccounts);
        }
    }
}