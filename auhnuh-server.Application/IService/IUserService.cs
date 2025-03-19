using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebResponse.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.IService
{
    [AutoRegister]
    public interface IUserService
    {
        Task<PagedModel<UserDTO>> ListUserAdmin(int pageSize, int pageNumber, string? term);
    }
}
