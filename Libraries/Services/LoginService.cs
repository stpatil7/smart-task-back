using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccessLayer.Entities;
using Domain.Dto.LoginDtos;
using Domain.Interfaces.Login;
using Domain.Interfaces.Repository;
using Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Libraries.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<Users> _usersRepository;
        private readonly string? _encryptionKey;
        private readonly JwtSettingsDto _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly string? _otpSecurityKey;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailTemplateRenderer _emailTemplateRenderer;
        private readonly string? _jwtEncryptionKey;
        public LoginService(IRepository<Users> usersRepository,
            IConfiguration configuration,
            IOptions<JwtSettingsDto> jwtSettings,
            IEmailService emailService,
            IMemoryCache memoryCache,
            IEmailTemplateRenderer emailTemplateRenderer)
        {
            _usersRepository = usersRepository;
            _encryptionKey = configuration["EncryptionKeys:DefaultKey"];
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _otpSecurityKey = configuration["optKey"];
            _memoryCache = memoryCache;
            _emailTemplateRenderer = emailTemplateRenderer;
            _jwtEncryptionKey = configuration["EncryptionKeys:JwtEncrypptionKey"];
        }


        public async Task<LoginResponseDto> Login(LoginDto model)
        {
            var user = await _usersRepository.Find(x => x.Email.Equals(model.Email)).FirstOrDefaultAsync();

            if (user == null)
                return null;

            string decryptedPassword = Utilities.Decrypt(user.PasswordHash, _encryptionKey);
            if (model.Password != decryptedPassword)
                return null;


            var role = user.role == (int)UserRolesEnum.Admin ? "Admin" : "User";

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryTime = DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes);

            var token = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiryTime,
                signingCredentials: creds
            );

            string writtenToken = new JwtSecurityTokenHandler().WriteToken(token);

            string encryptedToken = Utilities.Encrypt(writtenToken, _jwtEncryptionKey);

            return new LoginResponseDto
            {
                Token = encryptedToken,
                Role = role
            };
        }

        public async Task<int> RegisterUserAsync(UserRegistrationsDto model)
        {
            Users user = new Users();
            user.Name = model.Name;
            user.Email = model.Email;
            user.PasswordHash = Utilities.Encrypt(model.Password, _encryptionKey);
            user.role = model.role;
            user.CreatedAt = DateTime.Now;
            user.UpdateAt = DateTime.Now;

            await _usersRepository.AddAsync(user);
            await _usersRepository.SaveAsync();

            return user.Id;
        }

        public async Task<string> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _usersRepository.Find(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return "Invalid User";
            }

            string otp = Utilities.GenerateOtp();

            string encryptedOtp = Utilities.Encrypt(otp, _encryptionKey);

            _memoryCache.Set(_otpSecurityKey, encryptedOtp, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            var emailHtml = await _emailTemplateRenderer.RenderOtpTemplateAsync(otp);
            await _emailService.SendEmailAsync(model.Email, "Password Reset", emailHtml);


            return "Otp Sent";
        }

        public string VerifyOtp(string otp)
        {
            if (!_memoryCache.TryGetValue(_otpSecurityKey, out string? cachedOtp))
                return "OTP expired or not found";

            string decryptedOtp = Utilities.Decrypt(cachedOtp, _encryptionKey);

            if (decryptedOtp != otp)
                return "Invalid OTP";

            return "OTP verification successful";
        }

        public async Task<string> ResetPassword(ResetPasswordDto model)
        {
            if (!_memoryCache.TryGetValue(_otpSecurityKey, out string? cachedOtp))
                return "OTP expired or not found";

            string decryptedOtp = Utilities.Decrypt(cachedOtp, _encryptionKey);

            if (decryptedOtp != model.Otp)
                return "Invalid OTP";

            var user = await _usersRepository.Find(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (user == null)
                return "Invalid User";

            user.PasswordHash = Utilities.Encrypt(model.ConfirmPassword, _encryptionKey);

            _usersRepository.UpdateAsync(user);
            await _usersRepository.SaveAsync();

            _memoryCache.Remove(_otpSecurityKey);

            return "Password reset complete";
        }
    }
}
