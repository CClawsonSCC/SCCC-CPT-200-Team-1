

namespace CodeCrateData {

    public class ActiveLogService {
        List<ActiveLog> activeLogsList = new List<ActiveLog>(); // Main Dictionary
        String userAccountCsvFilePath = "CodeCrateData/ActiveLoggingSystem/ActiveLogs.csv";

        CodeCrateDataCsv _activeLogCsv;
        
        ActiveLog activeLog = new ActiveLog();
        public ActiveLogService(CodeCrateDataCsv activeLogCsv) {
            _activeLogCsv = activeLogCsv;
        }

        public async Task loadUpLogs() {
            activeLogsList = (await _activeLogCsv.LoadCollection<ActiveLog>(userAccountCsvFilePath)).ToList();
        }

        public async Task addLog(UserAccount userAccount, string logEvent) {

            activeLog.CurrentDateTime = DateTime.Now;
            activeLog.CurrentUserID = userAccount.UserID;
            activeLog.CurrentUsername = userAccount.Username;
            activeLog.CurrentEventAction = logEvent;

            activeLogsList.Add(activeLog);
            await _activeLogCsv.WriteCollection<ActiveLog>(activeLogsList, userAccountCsvFilePath);
            await loadUpLogs();
        }




    }
    



}