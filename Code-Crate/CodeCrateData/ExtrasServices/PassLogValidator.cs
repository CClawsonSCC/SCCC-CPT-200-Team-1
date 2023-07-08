
using System.Linq;

namespace CodeCrateData {

    public class PassLogValidator {

        List<string> messagePrompts = null!;
        
        private PasswordLogService _passLogService;

        public PassLogValidator(PasswordLogService passLogServce)
        {
            _passLogService = passLogServce;
            
        }

        public List<string> ItemWarnings(PasswordLog passLog, string switchCase) {
            messagePrompts = new List<string>();

            switch (switchCase) {

                case "DeleteWarning":
                    messagePrompts.Add("Are you sure you want to delete this credential?");
                    return messagePrompts;

            }
            return messagePrompts;
        }
    }
}