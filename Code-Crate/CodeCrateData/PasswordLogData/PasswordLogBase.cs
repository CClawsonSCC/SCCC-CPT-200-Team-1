using System.ComponentModel.DataAnnotations;

namespace CodeCrateData {
    public class PasswordLog
    {   
        public int PassID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "Application must be required")]
        [MaxLength(18)]
        public string Application {get; set;} = "";

        
        [Required(ErrorMessage = "Username must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Username can't contain spaces!")]
        [MaxLength(18)]
        [MinLength(4)]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        [MaxLength(18)]
        [MinLength(8)]
        public string Password { get; set; } = "";



    }
    

    
      
}