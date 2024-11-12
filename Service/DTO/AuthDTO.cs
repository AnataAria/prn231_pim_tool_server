using DataAccessLayer.BusinessObject;

namespace Service.DTO
{
    public class AuthenticationRequest
    {
        public string? UserName {get; set;}
        public string? Password {get; set;}
    }
    public class AuthenticationResponse
    {
        public string? Token {get;set;}
        public UserRole Role {get; set;}

    }
}
