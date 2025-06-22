using auhnuh_server.Application.IService;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.User;
using auhnuh_server.Domain.DTO.WebResponse.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auhnuh_server.API.Controllers
{
    [Route("api/users")]
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

        [HttpGet("user-detail")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            var response = await _userService.AddUser(userDTO, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO userDTO, CancellationToken cancellationToken)
        {
            var response = await _userService.UpdateUser(id, userDTO, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteUser(id, cancellationToken);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("ban-user")]
        public async Task<IActionResult> BanUser(int id, CancellationToken cancellationToken)
        {
            var response = await _userService.BanUser(id, cancellationToken);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
