using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace CodeCrateData {
    public class CredentialLog
    {   
        // This index tag helps the CsvHelper determine where to put these values on the csv file.
        [Index(0)]
        public int PassID { get; set; }
        [Index(1)]
        public int UserID { get; set; }

        [Index(2)]
        [Required(ErrorMessage = "Application must be required")]
        [MaxLength(18)]
        public string Application {get; set;} = "";

        [Index(3)]
        [Required(ErrorMessage = "Username must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Username can't contain spaces!")]
        [MaxLength(18)]
        [MinLength(4)]
        public string Username { get; set; } = "";

        [Index(4)]
        [Required(ErrorMessage = "Password must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        [MaxLength(18, ErrorMessage = "Maximum length is 18 characters")]
        [MinLength(8, ErrorMessage = "Minimal length is 8 characters")]
        public string Password { get; set; } = "";



    }
    

    
      
}