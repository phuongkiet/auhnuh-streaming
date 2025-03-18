using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Auth;
using auhnuh_server.Domain.DTO.WebResponse.Auth;
using auhnuh_server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace auhnuh_server.Infrastructure.Repository
{
    [AutoRegister]
    public class AuthRepository : IAuthRepository
    {
        private readonly MovieDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SymmetricSecurityKey _key;

        const string EMAIL_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public AuthRepository(MovieDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));
        }

        public async Task<ApiResponseModel<LoginResponseDTO>> Login(LoginDTO login)
        {
            var response = new ApiResponseModel<LoginResponseDTO>();

            if(login.Email == null)
            {
                response.Errors.Add("Please enter your email first. Email must not null");
                return response;
            } else if (!IsEmail(login.Email))
            {
                response.Errors.Add("This email is invalid. Please enter the valid email format.");
                return response;
            } else if(login.Password == null)
            {
                response.Errors.Add("Please enter your password first. Password must not null");
                return response;
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null)
            {
                response.Errors.Add("User with email: " + login.Email + " does not exist!");
                return response;
            } else if (!user.EmailConfirmed)
            {
                response.Errors.Add("Your email is not verified");
                return response;
            } else if (user.Status  != UserStatus.Active)
            {
                response.Errors.Add("Your account has been banned or deactived by some reasons. Please contact with Administrator for detail!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                response.Errors.Add("Wrong password! Please try again.");
                return response;
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var loginAcc = new LoginResponseDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Birthday = user.Birthday,
                Name = user.Name,
                Email = user.Email,
                Token = CreateToken(user),
                PhoneNumber = user.PhoneNumber,
                Status = user.Status.ToString(),
                Role = userRole.ToList()
            };
            response.Data = loginAcc;

            return response;
        }

        public async Task<ApiResponseModel<AccountResponseDTO>> GetCurrentUser(string email)
        {
            var result = new ApiResponseModel<AccountResponseDTO>();

            try
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.Email == email);
                
                await _userManager.UpdateAsync(user);

                //Get roles
                var userRole = await _userManager.GetRolesAsync(user);

                var currentUser = new AccountResponseDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Birthday = user.Birthday,
                    Name = user.Name,
                    Email = user.Email,
                    Token = CreateToken(user),
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status.ToString(),
                    Role = userRole.ToList()
                };
                result.Data = currentUser;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public string CreateToken(User user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
        };

            claims.AddRange(roles.Select(c => new Claim(ClaimTypes.Role, c)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = creds,
            };

            var tokenHandle = new JwtSecurityTokenHandler();

            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }

        public bool IsEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, EMAIL_REGEX, RegexOptions.IgnoreCase);
            return isEmail;
        }
    }
}
