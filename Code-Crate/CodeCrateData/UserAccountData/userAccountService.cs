namespace CodeCrateData {

    public class UserAccountService {   

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>(); // Main Dictionary
        int accountNum;

        UserAccountDataCsv _userAccountCsv;

        public UserAccountService(UserAccountDataCsv userAccountCsv) {
            _userAccountCsv = userAccountCsv;
        }

        public async Task<Dictionary<int, UserAccount>> GetUserAccounts() {  
                userAccountDict = (await _userAccountCsv.LoadCollection()).ToDictionary(r => r.UserID, r => r);
                return userAccountDict;
        }
        // Register a new user
        public async Task AddUserAccount(UserAccount userAccount) {  
            var lastId = userAccountDict.Count() == 0 ? 0 : userAccountDict.Keys.Max();
            userAccount.UserID = lastId + 1;
            userAccountDict.Add(userAccount.UserID, userAccount);
            await _userAccountCsv.WriteCollection(userAccountDict.Values);
            
        }


        public async Task<bool> VerifyUserAccount(UserAccount userAccount) {
            var userAccountDictCopy = await GetUserAccounts();
            
            foreach (var accounts in userAccountDictCopy.Values)
            {   
                accountNum++;
                if (accounts.Username == userAccount.Username && accounts.Password == userAccount.Password) {
                    return await Task.FromResult(true);
                    

                }
                
            }
            return await Task.FromResult(false);
        }
    
        public async Task<UserAccount> GetCurrentAccount(UserAccount userAccount) {
            var test = await VerifyUserAccount(userAccount);
            if ( test == true ){
                return await Task.FromResult(userAccountDict[accountNum]);
            }
            return await Task.FromResult(userAccountDict[0]);
            
        }

        public Task<UserAccount> GetAccountData(int id) {   
            return Task.FromResult(userAccountDict[id]);
        }



    }
}