namespace Libraries.Interfaces
{
    public interface IEmailTemplateRenderer
    {
        Task<string> RenderOtpTemplateAsync(string otp);
    }
}
