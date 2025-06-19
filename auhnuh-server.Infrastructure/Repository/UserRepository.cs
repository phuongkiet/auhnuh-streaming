using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.User;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using auhnuh_server.Domain.DTO.WebResponse.User;
using auhnuh_server.Infrastructure.Data;
using auhnuh_server.Infrastructure.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Infrastructure.Repository
{
    [AutoRegister]
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<UserDTO>> ListUserAdmin(int pageSize, int pageNumber, string? term)
        {
            if (pageSize == 0 && pageNumber == 0) pageSize = int.MaxValue; pageNumber = 1;

            var query = _context.CreateSet<User>()
                .Include(r => r.Role)
                .AsEnumerable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(u => u.Name.Contains(term));
            }

            var totalItems = query.Count();

            var response = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            var result = _mapper.Map<List<UserDTO>>(response);

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedModel = new PagedModel<UserDTO>
            {
                PageNo = pageNumber,
                TotalItems = totalItems,
                TotalPage = totalPages,
                Results = result,
            };

            return pagedModel;
        }

        public async Task<ApiResponseModel<UserDTO>> GetUserById(int id)
        {
            var response = new ApiResponseModel<UserDTO>();

            try
            {
                var user = await _context.CreateSet<User>()
                    .Include(r => r.Role)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    response.Errors.Add("User not found.");
                    return response;
                }

                response.Data = _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while retrieving the user.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> AddUser(AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var existingUser = await _context.CreateSet<User>()
                    .FirstOrDefaultAsync(u => u.Email == userDTO.Email || u.Name == userDTO.Name || u.PhoneNumber == userDTO.PhoneNumber, cancellationToken);

                if (existingUser == null)
                {
                    var entity = _mapper.Map<User>(userDTO);
                    entity.Status = UserStatus.Active;

                    await _context.CreateSet<User>().AddAsync(entity, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    response.Data = "User added successfully.";
                }
                else
                {
                    response.Errors.Add("This user already exist!");
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while adding the user data.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> UpdateUser(int id, UpdateUserDTO userDTO, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var user = await _context.CreateSet<User>()
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

                if (user == null)
                {
                    response.Errors.Add("User not found.");
                    return response;
                }

                // Update user values
                user.Name = userDTO.Name;
                user.PhoneNumber = userDTO.PhoneNumber;
                user.Email = userDTO.Email;
                user.Birthday = userDTO.Birthday;
                user.Status = userDTO.Status;
                user.RoleId = userDTO.RoleId;

                _context.CreateSet<User>().Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                response.Data = "User updated successfully.";
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while updating the user.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var user = await _context.CreateSet<User>()
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

                if (user == null)
                {
                    response.Errors.Add("User not found.");
                    return response;
                }

                _context.CreateSet<User>().Remove(user);
                await _context.SaveChangesAsync(cancellationToken);

                response.Data = "User deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while deleting the user.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> BanUser(int id, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var user = await _context.CreateSet<User>()
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

                if (user == null)
                {
                    response.Errors.Add("User not found.");
                    return response;
                }

                // Update user values
                if (user.Status == UserStatus.Active)
                {
                    user.Status = UserStatus.Banned;
                    response.Data = "User Banned";
                } else if (user.Status == UserStatus.Banned)
                {
                    user.Status = UserStatus.Active;
                    response.Data = "User Unbanned";
                }
                else
                {
                    response.Errors.Add("User status cannot be changed.");
                    return response;
                }

                _context.CreateSet<User>().Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while updating the user.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

    }
}
