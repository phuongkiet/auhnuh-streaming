using auhnuh_server.Application.IRepository;
using auhnuh_server.Application.IService;
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

namespace auhnuh_server.Application.Service
{
    [AutoRegister]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedModel<UserDTO>> ListUserAdmin(int pageSize, int pageNumber, string? term)
        {
            return await _userRepository.ListUserAdmin(pageSize, pageNumber, term);
        }

        public async Task<ApiResponseModel<UserDTO>> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<ApiResponseModel<string>> AddUser(AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            return await _userRepository.AddUser(userDTO, cancellationToken);
        }

        public async Task<ApiResponseModel<string>> UpdateUser(int id, UpdateUserDTO userDTO, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUser(id, userDTO, cancellationToken);
        }

        public async Task<ApiResponseModel<string>> DeleteUser(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUser(id, cancellationToken);
        }

        public async Task<ApiResponseModel<string>> BanUser(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.BanUser(id, cancellationToken);
        }
    }
}
