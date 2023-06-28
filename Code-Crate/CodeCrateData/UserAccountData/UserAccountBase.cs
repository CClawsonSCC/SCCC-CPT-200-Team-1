using System.ComponentModel.DataAnnotations;

namespace CodeCrateData {
    public class UserAccount
    {   
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username must be required")]
        [MaxLength(25)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        [MaxLength(16)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Please confirm the password")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        public string ConfirmPassword { get; set; } = "";

        [Required(ErrorMessage = "Email must be required")]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid email address.")]

        [MaxLength(25)]
        public string Email {get; set;} = null!;
    }
    

    
      
}