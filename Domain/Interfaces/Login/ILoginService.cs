using Domain.Dto.LoginDtos;

namespace Domain.Interfaces.Login
{
    public interface ILoginService
    {
        Task<int> RegisterUserAsync(UserRegistrationsDto model);
        Task<LoginResponseDto> Login(LoginDto model);
        Task<string> ForgotPassword(ForgotPasswordDto model);
        Task<string> ResetPassword(ResetPasswordDto model);
        Task<string> VerifyOtp(string otp);
    }
}
