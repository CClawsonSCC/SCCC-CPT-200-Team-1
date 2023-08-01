namespace CodeCrateData {

    public class UserAccountService {   

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>(); // Main Dictionary
        String userAccountCsvFilePath = "CodeCrateData/UserAccountData/UserAccount.csv";

        CodeCrateDataCsv _userAccountCsv;
        ActiveLogService _activeLog;
        int accountNum = 0;

        // This method initializes services that will be used here
        public UserAccountService(CodeCrateDataCsv userAccountCsv, ActiveLogService activeLog) {
            _userAccountCsv = userAccountCsv;
            _activeLog = activeLog;
        }

        // This will keep the CSV from overriding on each time the home page is loaded up.
        // It loads up the current values in the CSV file and stores them in the dictionary.
        // Everytime this app is loaded-up a new instance of the dictionary is created, but if we immediately fill that dictionary up with values in the CSV file we will be good to go.
        public async Task LoadUserAccounts() {
                userAccountDict = (await _userAccountCsv.LoadCollection<UserAccount>(userAccountCsvFilePath)).ToDictionary(r => r.UserID, r => r);
                accountNum = 0;
        }

        // Register a new user and create an Activelog for it
        // It takes the last userID and adds 1 to it in order to increment the dictionary
        // If there are no entries, then it will start at 1.
        // Before a user account is added it goes through a duplicate check to make sure there are no duplicates
        public async Task AddUserAccount(UserAccount userAccount) {  
            var lastId = userAccountDict.Count() == 0 ? 0 : userAccountDict.Keys.Max();
            userAccount.UserID = lastId + 1;
            userAccountDict.Add(userAccount.UserID, userAccount);
            await _userAccountCsv.WriteCollection<UserAccount>(userAccountDict.Values, userAccountCsvFilePath);
            await _activeLog.accountLog(userAccount, "Account has been Created!");
        }

        // This verification check takes the current account in the login box and checks the database for a match
        // If the current account object returns true the user will be logged in.
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

        // Once it passes the verifcation check it will run this method in order to get the current account
        public async Task<UserAccount> GetCurrentAccount(UserAccount userAccount) {
                return await Task.FromResult(userAccountDict[accountNum]);
            }


        // This is called in the Maindashboard page to where we can get all of the current user's credentials
        // This is tied by the userID
        public Task<UserAccount> GetAccountData(int id) {   
            return Task.FromResult(userAccountDict[id]);
        }

        // This method is the duplicate check that happens when you try to register a new account
        // It checks to make sure the username or email aren't used twice.
        // If it returns true it will tell the user that there is a duplicate
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