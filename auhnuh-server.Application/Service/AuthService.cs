using auhnuh_server.Application.IRepository;
using auhnuh_server.Application.IService;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Auth;
using auhnuh_server.Domain.DTO.WebResponse.Auth;


namespace auhnuh_server.Application.Service
{
    [AutoRegister]
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository) 
        {
            _authRepository = authRepository;
        }

        public async Task<ApiResponseModel<LoginResponseDTO>> Login(LoginDTO login)
        {
            return await _authRepository.Login(login);
        }

        public async Task<ApiResponseModel<AccountResponseDTO>> GetCurrentUser(string email)
        {
            return await _authRepository.GetCurrentUser(email);
        }
    }
}
