using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Auth;
using auhnuh_server.Domain.DTO.WebResponse.Auth;

namespace auhnuh_server.Application.IService
{
    [AutoRegister]
    public interface IAuthService
    {
        Task<ApiResponseModel<LoginResponseDTO>> Login(LoginDTO login);
    }
}
