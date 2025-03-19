using auhnuh_server.Application.IService;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebResponse.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auhnuh_server.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAdmin(int pageSize, int pageNumber, string? term)
        {
            var response = await _userService.ListUserAdmin(pageSize, pageNumber, term);
            var result = new ApiResponseModel<PagedModel<UserDTO>>();
            if (response == null)
            {
                result.Errors.Add("There is no users!");
                return BadRequest(result);
            }
            else
            {
                result.Data = response;
                return Ok(result);
            }
        }
    }
}
