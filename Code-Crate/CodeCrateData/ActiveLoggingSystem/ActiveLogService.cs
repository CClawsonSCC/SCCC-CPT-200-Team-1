

namespace CodeCrateData {

    public class ActiveLogService {

        Dictionary<int, UserAccount> userAccountDict = new Dictionary<int, UserAccount>();
        Dictionary<int, PasswordLog> credentialDict = new Dictionary<int, PasswordLog>();
        List<ActiveLog> activeLogsList = new List<ActiveLog>(); // Main Dictionary
        String activeLogCsvPath = "CodeCrateData/ActiveLoggingSystem/ActiveLogs.csv";
        String userAccountCsvFilePath = "CodeCrateData/UseraccountData/UserAccount.csv";
        String credentialCsvFilePath = "CodeCrateData/PasswordLogData/PasswordLog.csv";



        CodeCrateDataCsv _codeCrateCsv;
        
        ActiveLog activeLog = new ActiveLog();
        public ActiveLogService(CodeCrateDataCsv codeCrateCsv) {
            _codeCrateCsv = codeCrateCsv;
        }

        public async Task loadUpLogs() {
            activeLogsList = (await _codeCrateCsv.LoadCollection<ActiveLog>(activeLogCsvPath)).ToList();
            userAccountDict = (await _codeCrateCsv.LoadCollection<UserAccount>(userAccountCsvFilePath)).ToDictionary(r => r.UserID, r => r);
            credentialDict = (await _codeCrateCsv.LoadCollection<PasswordLog>(credentialCsvFilePath)).ToDictionary(r => r.PassID, r => r);
        }

        public async Task accountLog(UserAccount userAccount, string logEvent) {
            await loadUpLogs();
            activeLog.CurrentDateTime = DateTime.Now;
            activeLog.CurrentUserID = $"User-ID: {userAccount.UserID}";
            activeLog.CurrentUsername = $"Username: {userAccount.Username}";
            activeLog.CurrentEventAction = logEvent;

            activeLogsList.Add(activeLog);
            await _codeCrateCsv.WriteCollection<ActiveLog>(activeLogsList, activeLogCsvPath);
            
        }

        public async Task credentialLog(int passLogID, int userID, int isDeletedAdded) {
            await loadUpLogs();
            var credential = credentialDict[passLogID];
            var userAccount = userAccountDict[userID];
            activeLog.CurrentDateTime = DateTime.Now;
            activeLog.CurrentUserID = $"User-ID: {userAccount.UserID}";
            activeLog.CurrentUsername = $"Username: {userAccount.Username}";
      
            if(isDeletedAdded == 0) {
                activeLog.CurrentEventAction = $"Added a new credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";

            }
            else if (isDeletedAdded == 1) {
                activeLog.CurrentEventAction = $"Deleted a credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";
            }
            else {
                activeLog.CurrentEventAction = $"Updated a credential: {credential.PassID}, {credential.Application}, {credential.Username}, {credential.Password}";
 
            }

            activeLogsList.Add(activeLog);
            await _codeCrateCsv.WriteCollection<ActiveLog>(activeLogsList, activeLogCsvPath);
            
        }




    }
    



}