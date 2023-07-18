namespace CodeCrateData {

    public class PasswordLogService {   

        Dictionary<int, PasswordLog> passwordLogDict = new Dictionary<int, PasswordLog>(); // Main Dictionary
        CodeCrateDataCsv _passLogCsv;
        
        Cipher _cipher;
        String passLogCsvFilePath = "CodeCrateData/PasswordLogData/PasswordLog.csv";

        public PasswordLogService(CodeCrateDataCsv passwordLogCsv, Cipher cipher) {
            _passLogCsv = passwordLogCsv;
            _cipher = cipher;
            
            
        }

        // This will keep the CSV from overriding on each time the home page is loaded up.
        // It loads up the current values in the CSV file and stores them in the dictionary.
        // Everytime this app is loaded-up a new instance of the dictionary is created, but if we immediately fill that dictionary up with values in the CSV file we will be good to go.
        public async Task<IEnumerable<PasswordLog>> GetUserPasswords(int userID) {
            passwordLogDict = (await _passLogCsv.LoadCollection<PasswordLog>(passLogCsvFilePath)).ToDictionary(r => r.PassID, r => r);
            /*
            userCredentials.Clear();
            foreach (var credential in passwordLogDict.Values) {
                if (userID == credential.UserID)
                {   
                    incrementDictKeys++;
                    userCredentials.Add(incrementDictKeys, credential);
                }
            }
            return userCredentials.Values;
            */
            
            return passwordLogDict.Values.Where(x => x.UserID == userID);
             
        }

        // Register a new user
        public async Task AddUserPassword(PasswordLog passLog, int userID) {
        
            var lastId = passwordLogDict.Count() == 0 ? 0 : passwordLogDict.Keys.Max();
            passLog.PassID = lastId + 1;
            passLog.UserID = userID;
            var testCipher = _cipher.Encrypt(passLog.Password);
            passLog.Password = testCipher;
            passwordLogDict.Add(passLog.PassID, passLog);
            
            await _passLogCsv.WriteCollection<PasswordLog>(passwordLogDict.Values, passLogCsvFilePath);
            
        }

        public Task<PasswordLog> GetPassLogData(int id) {
            var currentCredential = passwordLogDict[id];
            var decryptCredentialPassword = _cipher.Decrypt(currentCredential.Password);
            currentCredential.Password = decryptCredentialPassword;
            passwordLogDict[id] = currentCredential;
            return Task.FromResult(passwordLogDict[id]);
        }

        public async Task UpdatePassLog(PasswordLog passLog) {
            var testCipher = _cipher.Encrypt(passLog.Password);
            passLog.Password = testCipher;
            passwordLogDict[passLog.PassID] = passLog;
            await _passLogCsv.WriteCollection<PasswordLog>(passwordLogDict.Values, passLogCsvFilePath);

            
        }

        public async Task DeletePassLog(int id) {
            passwordLogDict.Remove(id);
            await _passLogCsv.WriteCollection<PasswordLog>(passwordLogDict.Values, passLogCsvFilePath);
            passwordLogDict = (await _passLogCsv.LoadCollection<PasswordLog>(passLogCsvFilePath)).ToDictionary(r => r.PassID, r => r);
        }
    }
}