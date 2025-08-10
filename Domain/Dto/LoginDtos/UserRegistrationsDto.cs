using Libraries.Enums;

namespace Domain.Dto.LoginDtos
{
    public class UserRegistrationsDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRolesEnum role { get; set; }
    }
}
