

namespace CodeCrateData {

    public class ActiveLogService {

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>();
        Dictionary<int, CredentialLog> credentialDict = new Dictionary<int, CredentialLog>();
        List<ActiveLog> activeLogsList = new List<ActiveLog>(); // Main activeLog Dictionary
        String activeLogCsvFilePath = "CodeCrateData/ActiveLoggingSystem/ActiveLogs.csv";
        String userAccountCsvFilePath = "CodeCrateData/UserAccountData/UserAccount.csv";
        String credentialCsvFilePath = "CodeCrateData/CredentialLogData/CredentialLog.csv";



        CodeCrateDataCsv _codeCrateCsv;
        
        ActiveLog activeLog = new ActiveLog();
        public ActiveLogService(CodeCrateDataCsv codeCrateCsv) {
            _codeCrateCsv = codeCrateCsv; // This initializes the service on startup
        }

        // This will make sure all the dictionaries are current when called upon.
        public async Task loadUpLogs() {
            activeLogsList = (await _codeCrateCsv.LoadCollection<ActiveLog>(activeLogCsvFilePath)).ToList();
            userAccountDict = (await _codeCrateCsv.LoadCollection<UserAccount>(userAccountCsvFilePath)).ToDictionary(r => r.UserID, r => r);
            credentialDict = (await _codeCrateCsv.LoadCollection<CredentialLog>(credentialCsvFilePath)).ToDictionary(r => r.PassID, r => r);
        }

        // This will store any activeLogs that deals with account only into the csv file
        public async Task accountLog(UserAccount userAccount, string logEvent) {
            await loadUpLogs();
            activeLog.CurrentDateTime = DateTime.Now;
            activeLog.CurrentUserID = $"User-ID: {userAccount.UserID}";
            activeLog.CurrentUsername = $"Username: {userAccount.Username}";
            activeLog.CurrentEventAction = logEvent;

            activeLogsList.Add(activeLog);
            await _codeCrateCsv.WriteCollection<ActiveLog>(activeLogsList, activeLogCsvFilePath);
            
        }

        // This will store any activeLogs that deals with credentialLogs into the csv file.
        public async Task credentialLog(int passLogID, int userID, int isDeletedAdded) {
            await loadUpLogs();
            var credential = credentialDict[passLogID];
            var userAccount = userAccountDict[userID];
            activeLog.CurrentDateTime = DateTime.Now;
            activeLog.CurrentUserID = $"User-ID: {userAccount.UserID}";
            activeLog.CurrentUsername = $"Username: {userAccount.Username}";

            // This will be "Added" a new credential if == 0
            if(isDeletedAdded == 0) {
                activeLog.CurrentEventAction = $"Added a new credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";

            }
            // This will be "Deleted" a new credential if == 1
            else if (isDeletedAdded == 1) {
                activeLog.CurrentEventAction = $"Deleted a credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";
            }
            // This will be "Updated" a new credential if == anything else
            else {
                activeLog.CurrentEventAction = $"Updated a credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";
 
            }

            activeLogsList.Add(activeLog);
            await _codeCrateCsv.WriteCollection<ActiveLog>(activeLogsList, activeLogCsvFilePath);
            
        }




    }
    



}