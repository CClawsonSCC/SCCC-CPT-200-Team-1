namespace CodeCrateData {

    public class UserAccountService {   

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>(); // Main Dictionary

        UserAccountDataCsv _userAccountCsv;

        public UserAccountService(UserAccountDataCsv userAccountCsv) {
            _userAccountCsv = userAccountCsv;
        }
        // Register a new user
        public async Task AddUserAccount(UserAccount userAccount) {    
            var lastId = userAccountDict.Count() == 0 ? 0 : userAccountDict.Keys.Max();
            userAccount.UserID = lastId + 1;
            userAccountDict.Add(userAccount.UserID, userAccount);
            await _userAccountCsv.WriteCollection(userAccountDict.Values);
        }
    }

}