using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.User;
using auhnuh_server.Domain.DTO.WebResponse.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.IRepository
{
    [AutoRegister]
    public interface IUserRepository
    {
        Task<PagedModel<UserDTO>> ListUserAdmin(int pageSize, int pageNumber, string? term);
        Task<ApiResponseModel<UserDTO>> GetUserById(int id);
        Task<ApiResponseModel<string>> AddUser(AddUserDTO userDTO, CancellationToken cancellationToken);
        Task<ApiResponseModel<string>> UpdateUser(int id, UpdateUserDTO userDTO, CancellationToken cancellationToken);
        Task<ApiResponseModel<string>> DeleteUser(int id, CancellationToken cancellationToken);
        Task<ApiResponseModel<string>> BanUser(int id, CancellationToken cancellationToken);
    }
}
