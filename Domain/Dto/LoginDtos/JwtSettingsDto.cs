namespace Domain.Dto.LoginDtos
{
    public class JwtSettingsDto
    {
        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public int ExpireMinutes { get; set; }
    }

}
