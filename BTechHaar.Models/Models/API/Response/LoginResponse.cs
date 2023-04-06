namespace BTechHaar.Models.API.Response
{
    public class LoginResponse
    {
        public string EmailId { get; set; }
        public bool IsValidUser { get; set; }
        public string DeviceId { get; set; }
        public bool IsEmailVerified { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string OTPText { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
