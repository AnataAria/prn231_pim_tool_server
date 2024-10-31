using DataAccessLayer.BusinessObject;

namespace Service.DTO.Response
{
    public class AuthenticationResponse
    {
        public string? Token {get;set;}
        public UserRole Role {get; set;}

    }
}
