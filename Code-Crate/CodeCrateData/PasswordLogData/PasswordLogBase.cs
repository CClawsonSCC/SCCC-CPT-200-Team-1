using System.ComponentModel.DataAnnotations;

namespace CodeCrateData {
    public class PasswordLog
    {   
        public int PassID { get; set; }

        public int UserID { get; set; }

        //[Required(ErrorMessage = "Application must be required")]
        //[MaxLength(25)]
        public string Application {get; set;} = null!;

        //[Required(ErrorMessage = "Username must be required")]
        //[MaxLength(25)]
        public string Username { get; set; } = null!;

        //[Required(ErrorMessage = "Password must be required")]
        //[MaxLength(25)]
        public string Password { get; set; } = null!;



    }
    

    
      
}