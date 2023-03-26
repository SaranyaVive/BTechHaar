using Microsoft.Build.Framework;

namespace BTechHaar.Web.ViewModels.API.Request
{
    public class SignupRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string MPin { get; set; } = "";

    }
}
