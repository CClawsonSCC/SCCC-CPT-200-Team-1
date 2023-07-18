namespace CodeCrateData {

    public class CredentialLogService {   

        Dictionary<int, CredentialLog> credentialLogDict = new Dictionary<int, CredentialLog>(); // Main Dictionary
        CodeCrateDataCsv _credentialLogCsv;
        
        Cipher _cipher;
        ActiveLogService _activeLog;
        String credentialLogCsvFilePath = "CodeCrateData/CredentialLogData/CredentialLog.csv";

        public CredentialLogService(CodeCrateDataCsv credentialLogCsv, Cipher cipher, ActiveLogService activeLog) {
            _credentialLogCsv = credentialLogCsv;
            _cipher = cipher;
            _activeLog = activeLog;
            
        }

        // This will keep the CSV from overriding on each time the home page is loaded up.
        // It loads up the current values in the CSV file and stores them in the dictionary.
        // Everytime this app is loaded-up a new instance of the dictionary is created, but if we immediately fill that dictionary up with values in the CSV file we will be good to go.
        public async Task<IEnumerable<CredentialLog>> LoadAllUserCredentials(int userID) {
            credentialLogDict = (await _credentialLogCsv.LoadCollection<CredentialLog>(credentialLogCsvFilePath)).ToDictionary(r => r.PassID, r => r);
            
            return credentialLogDict.Values.Where(x => x.UserID == userID);
             
        }

        // Register a new user
        public async Task AddCredential(CredentialLog credLog, int userID) {
        
            var lastId = credentialLogDict.Count() == 0 ? 0 : credentialLogDict.Keys.Max();
            credLog.PassID = lastId + 1;
            credLog.UserID = userID;
            var testCipher = _cipher.Encrypt(credLog.Password);
            credLog.Password = testCipher;
            credentialLogDict.Add(credLog.PassID, credLog);
            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);
            await _activeLog.credentialLog(credLog.PassID, userID, 0);
        }

        public Task<CredentialLog> GetCurrentCredentialData(int id) {
            var currentCredential = credentialLogDict[id];
            var decryptCredentialPassword = _cipher.Decrypt(currentCredential.Password);
            currentCredential.Password = decryptCredentialPassword;
            credentialLogDict[id] = currentCredential;
            return Task.FromResult(credentialLogDict[id]);
        }

        public async Task UpdateCredential(CredentialLog credLog, int userID) {
            await encryptCredential(credLog);
            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);
            await _activeLog.credentialLog(credLog.PassID, userID, 3);

            
        }

        public async Task DeleteCredential(int id, int userID) {
            credentialLogDict.Remove(id);
            await _activeLog.credentialLog(id, userID, 1);

            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);
            credentialLogDict = (await _credentialLogCsv.LoadCollection<CredentialLog>(credentialLogCsvFilePath)).ToDictionary(r => r.PassID, r => r);
        }

        public async Task encryptCredential(CredentialLog credLog) {
            var testCipher = _cipher.Encrypt(credLog.Password);
            credLog.Password = testCipher;

            credentialLogDict[credLog.PassID] = credLog;
            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);

        }

    }
}