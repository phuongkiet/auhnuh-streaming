using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebResponse.Category;
using auhnuh_server.Infrastructure.Data.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace auhnuh_server.Infrastructure.Repository
{
    [AutoRegister]
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponseModel<List<CategoryDto>>> GetCategories()
        {
            var response = new ApiResponseModel<List<CategoryDto>>();
            try
            {
                var category = await _context.CreateSet<Category>().ToListAsync();
                if(category == null)
                {
                    response.Errors.Add("There are no categories.");
                    return response;
                }
                else
                {
                    var categories = _mapper.Map<List<CategoryDto>>(category);
                    response.Data = categories;
                    return response;
                }
            }catch(Exception ex)
            {
                response.Errors.Add("Got some errors: " + ex);
                return response;
            }
        }
    }
}
