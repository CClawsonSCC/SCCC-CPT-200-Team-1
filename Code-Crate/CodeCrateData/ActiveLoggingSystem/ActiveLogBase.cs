

using CsvHelper.Configuration.Attributes;

namespace CodeCrateData {

    // This class represents each property of the activeLog object that will be stored in the CSV file.
    public class ActiveLog {

        // This index tag helps the CsvHelper determine where to put these values on the csv file.
        [Index(0)]
        public DateTime? CurrentDateTime { get; set; } = null;
        [Index(1)]
        public string CurrentUserID { get; set; } = null!;
        [Index(2)]
        public string CurrentUsername { get; set; } = null!;
        [Index(3)]
        public string CurrentEventAction { get; set; } = null!;

    }
}