using Domain.Interfaces.Login;
using RazorLight;

namespace Libraries.Services
{
    public class EmailTemplateRenderer : IEmailTemplateRenderer
    {
        private readonly RazorLightEngine _engine;

        public EmailTemplateRenderer()
        {
            var baseDir = AppContext.BaseDirectory;
            var templatesRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "Application", "Templates"));


            _engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(templatesRoot)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderOtpTemplateAsync(string otp)
        {
            string templatePath = "OtpEmailTemplate.cshtml";
            string result = await _engine.CompileRenderAsync(templatePath, otp);
            return result;
        }
    }
}
