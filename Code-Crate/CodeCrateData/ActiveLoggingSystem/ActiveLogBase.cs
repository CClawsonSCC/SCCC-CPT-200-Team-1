

namespace CodeCrateData {

    public class ActiveLog {

        public DateTime? CurrentDateTime { get; set; } = null;

        public int CurrentUserID { get; set; }

        public string CurrentUsername { get; set; } = null!;

        public string CurrentEventAction { get; set; } = null!;

    }
}