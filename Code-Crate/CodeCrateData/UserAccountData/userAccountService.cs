namespace CodeCrateData {

    public class UserAccountService {   

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>(); // Main Dictionary
        String userAccountCsvFilePath = "CodeCrateData/UseraccountData/UserAccount.csv";

        CodeCrateDataCsv _userAccountCsv;
        int accountNum = 0;
        public UserAccountService(CodeCrateDataCsv userAccountCsv) {
            _userAccountCsv = userAccountCsv;
        }

        // This will keep the CSV from overriding on each time the home page is loaded up.
        // It loads up the current values in the CSV file and stores them in the dictionary.
        // Everytime this app is loaded-up a new instance of the dictionary is created, but if we immediately fill that dictionary up with values in the CSV file we will be good to go.
        public async Task<Dictionary<int, UserAccount>> GetUserAccounts() {
                userAccountDict = (await _userAccountCsv.LoadCollection<UserAccount>(userAccountCsvFilePath)).ToDictionary(r => r.UserID, r => r);
                accountNum = 0;
                return userAccountDict;
        }

        // Register a new user
        public async Task AddUserAccount(UserAccount userAccount) {  
            var lastId = userAccountDict.Count() == 0 ? 0 : userAccountDict.Keys.Max();
            userAccount.UserID = lastId + 1;
            userAccountDict.Add(userAccount.UserID, userAccount);
            await _userAccountCsv.WriteCollection<UserAccount>(userAccountDict.Values, userAccountCsvFilePath);
            
        }


        public async Task<bool> VerifyUserAccount(UserAccount userAccount) {
            
            foreach (var accounts in userAccountDict.Values)
            {   
                accountNum++;
                if (accounts.Username == userAccount.Username && accounts.Password == userAccount.Password) {
                    return await Task.FromResult(true);
                }
            }
            accountNum = 0;
            return await Task.FromResult(false);
        }

        public async Task<UserAccount> GetCurrentAccount(UserAccount userAccount) {
                return await Task.FromResult(userAccountDict[accountNum]);
            }

        public Task<UserAccount> GetAccountData(int id) {   
            return Task.FromResult(userAccountDict[id]);
        }

        public async Task<bool> CheckAccountDuplicates(UserAccount userAccount) {
            foreach (var accounts in userAccountDict.Values)
            {   
                if (accounts.Username == userAccount.Username || accounts.Email == userAccount.Email) {
                    return await Task.FromResult(true);
                }
            }
            return await Task.FromResult(false);


        }

    }
}