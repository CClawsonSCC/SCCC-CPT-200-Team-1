
using System.Linq;

namespace CodeCrateData {

    // This class helps display the modal that pops up when trying to delete a credential
    // Even though we only use it for one message, this class is setup to make as many messages as you want
    // Then you can link it to the ModalMessageBox Component to display the message.
    public class CredentialLogMessenger {

        List<string> messagePrompts = null!;

        // This switch case method allows us to implement future Modal Messages for the user
        // So more warning messages, success messages, etc.
        // We currently only have one, but can easily add more.
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