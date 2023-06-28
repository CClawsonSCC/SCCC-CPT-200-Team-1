using System.ComponentModel.DataAnnotations;

namespace CodeCrateData {
    public class UserAccount
    {   
        public int UserID { get; set; }

        //[Required(ErrorMessage = "Username must be required")]
        //[MaxLength(25)]
        public string Username { get; set; } = null!;

        //[Required(ErrorMessage = "Password must be required")]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Password can't contain spaces!")]
        [MaxLength(16)]
        public string Password { get; set; } = null!;

        //[Required(ErrorMessage = "Email must be required")]
        //[MaxLength(25)]
        public string Email {get; set;} = null!;

        public static implicit operator int(UserAccount v)
        {
            throw new NotImplementedException();
        }
    }
    

    
      
}