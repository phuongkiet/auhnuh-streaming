using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebResponse.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.IRepository
{
    [AutoRegister]
    public interface ICategoryRepository
    {
        Task<ApiResponseModel<List<CategoryDto>>> GetCategories();
    }
}
