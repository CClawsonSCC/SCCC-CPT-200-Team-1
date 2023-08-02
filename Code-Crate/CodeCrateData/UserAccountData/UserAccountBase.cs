using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace CodeCrateData {
    
    // This is the object's properties for all accounts.
    public class UserAccount
    {   // This index tag helps the CsvHelper determine where to put these values on the csv file.
        // There are also error handling/ required properties set in place in order to prevent bad data being stored.
        [Index(0)]
        public int UserID { get; set; }

        [Index(1)]
        [Required(ErrorMessage = "Username must be required")]
        [MaxLength(25)]
        public string Username { get; set; } = null!;

        [Index(2)]
        [Required(ErrorMessage = "Password must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        [MaxLength(16)]
        public string Password { get; set; } = "";

        [Index(3)]
        [Required(ErrorMessage = "Please confirm the password")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        public string ConfirmPassword { get; set; } = "";

        [Index(4)]
        [Required(ErrorMessage = "Email must be required")]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid email address.")]
        [MaxLength(25)]
        public string Email {get; set;} = null!;
    }
    

    
      
}