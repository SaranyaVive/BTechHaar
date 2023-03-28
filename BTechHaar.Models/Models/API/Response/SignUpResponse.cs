namespace BTechHaar.Models.API.Response
{
    public class SignUpResponse
    {
        public string OTPText { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string EmailId { get; set; }
        public int? UserId { get; set; }
        public bool IsEmailVerified { get; set; } = false;

    }
}
