namespace Domain.Interfaces.Login
{
    public interface IEmailTemplateRenderer
    {
        Task<string> RenderOtpTemplateAsync(string otp);
    }
}
