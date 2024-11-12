using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;


namespace Service.Service
{
    public class AuthService (UserRepository userRepository, TokenService tokenService)
    {
        private readonly UserRepository userRepo = userRepository;
        private readonly TokenService tokenService = tokenService;

        public async Task<ResponseEntity<AuthenticationResponse>> Login (AuthenticationRequest request) {
            try {
                var user = await userRepo.SearchUserByUsername(request.UserName);
                if (!ValidatePassword(request.Password, user.PasswordHash)) {
                    throw new Exception("Password Is Not Correct");
                }
                string userToken = tokenService.GenerateToken(user);
                return ResponseEntity<AuthenticationResponse>.CreateSuccess(new AuthenticationResponse() {
                    Token = userToken,
                    Role = user.Role
                });
            } catch(Exception ex) {
                return ResponseEntity<AuthenticationResponse>.Other(ex.Message, 401);
            }
        }

        private bool ValidatePassword (string? originPassword, string? hashPassword) {
            return BCrypt.Net.BCrypt.Verify(originPassword, hashPassword);
        }
        
    }
}
