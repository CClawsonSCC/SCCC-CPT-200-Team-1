
using System.Linq;

namespace CodeCrateData {

    public class CredentialLogMessenger {

        List<string> messagePrompts = null!;

        public List<string> ItemWarnings(string switchCase) {
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