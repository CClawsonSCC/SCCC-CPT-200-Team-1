namespace CodeCrateData {

    public class CredentialLogService {   

        Dictionary<int, CredentialLog> credentialLogDict = new Dictionary<int, CredentialLog>(); // Main Dictionary
        CodeCrateDataCsv _credentialLogCsv;
        
        Cipher _cipher;
        ActiveLogService _activeLog;
        String credentialLogCsvFilePath = "CodeCrateData/CredentialLogData/CredentialLog.csv";


        // This will initialize all services that are needed for the credentialLog service
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

        // Register a new credential and create an Activelog for it
        // It takes the last passID and adds 1 to it in order to increment the dictionary
        // If there are no entries, then it will start at 1.
        // The credential is also encrpyted when added to the csv
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

        // When this is called from the MainDashboard it will retrieve the selected credential by using the PassID.
        // We get that credential by using the dictionary, then we decrypt the password in order for the user to use it
        // This credential data will be displayed to the user via EditAddComponent       
        public Task<CredentialLog> GetCurrentCredentialData(int id) {
            var currentCredential = credentialLogDict[id];
            var decryptCredentialPassword = _cipher.Decrypt(currentCredential.Password);
            currentCredential.Password = decryptCredentialPassword;
            credentialLogDict[id] = currentCredential;
            return Task.FromResult(credentialLogDict[id]);
        }

        // After the user has made their changes the current credential will encrpyt then save to the csv file.
        // After the save point the editAddComponent will go away and bring you back to the dashboard.
        // The table will refresh as well
        public async Task UpdateCredential(CredentialLog credLog, int userID) {
            await encryptCredential(credLog);
            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);
            await _activeLog.credentialLog(credLog.PassID, userID, 3);

            
        }

        // When the user clicks the Delete button that is on the table it will take the selected passID
        // It will remove the credential from the dictionary, create an activeLog, and update the csv file
        // After the csv has been updated, we load-up the main dictionary in order to have the current data
        public async Task DeleteCredential(int id, int userID) {
            credentialLogDict.Remove(id);
            await _activeLog.credentialLog(id, userID, 1);

            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);
            credentialLogDict = (await _credentialLogCsv.LoadCollection<CredentialLog>(credentialLogCsvFilePath)).ToDictionary(r => r.PassID, r => r);
        }

        // We call this method in order to encrpyt our data instead of having to write it in every other method.
        public async Task encryptCredential(CredentialLog credLog) {
            var testCipher = _cipher.Encrypt(credLog.Password);
            credLog.Password = testCipher;

            credentialLogDict[credLog.PassID] = credLog;
            await _credentialLogCsv.WriteCollection<CredentialLog>(credentialLogDict.Values, credentialLogCsvFilePath);

        }

    }
}