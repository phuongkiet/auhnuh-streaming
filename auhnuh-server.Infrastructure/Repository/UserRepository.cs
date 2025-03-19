using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebResponse.User;
using auhnuh_server.Infrastructure.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Infrastructure.Repository
{
    [AutoRegister]
    public class UserRepository : IUserRepository
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<UserDTO>> ListUserAdmin(int pageSize, int pageNumber, string? term)
        {
            if(pageSize == 0 && pageNumber == 0) pageSize = int.MaxValue; pageNumber = 1;

            var query = _context.Users.Include(r => r.Role).AsEnumerable();
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
    }
}
