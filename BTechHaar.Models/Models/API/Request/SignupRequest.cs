using System.ComponentModel.DataAnnotations;

namespace BTechHaar.Models.API.Request
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
        public string DeviceId { get; set; }
        public int DeviceType { get; set; }

    }
}
