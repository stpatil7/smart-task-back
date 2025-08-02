namespace Domain.Dto
{
    public class ResetPasswordDto
    {
        public string Otp { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
