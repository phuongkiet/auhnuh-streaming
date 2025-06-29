using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Category;
using auhnuh_server.Domain.DTO.WebResponse.Category;
using auhnuh_server.Infrastructure.Data.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


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

        public async Task<ApiResponseModel<List<CategoryDTO>>> GetCategories()
        {
            var response = new ApiResponseModel<List<CategoryDTO>>();
            try
            {
                var category = await _context.CreateSet<Category>().ToListAsync();
                if (category == null)
                {
                    response.Errors.Add("There are no categories.");
                    return response;
                }
                else
                {
                    var categories = _mapper.Map<List<CategoryDTO>>(category);
                    response.Data = categories;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("Got some errors: " + ex);
                return response;
            }
        }

        public async Task<ApiResponseModel<CategoryDTO>> GetCategoryById(int id)
        {
            var response = new ApiResponseModel<CategoryDTO>();

            try
            {
                var category = await _context.CreateSet<Category>()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    response.Errors.Add("Category not found.");
                    return response;
                }

                response.Data = _mapper.Map<CategoryDTO>(category);

            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while retrieving the user.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> AddCategory(RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var existingCategory = await _context.CreateSet<Category>()
                    .FirstOrDefaultAsync(c => c.Name == requestCategoryDTO.Name, cancellationToken);

                if (existingCategory == null)
                {
                    var category = _mapper.Map<Category>(requestCategoryDTO);

                    await _context.CreateSet<Category>().AddAsync(category, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    response.Data = "User added successfully.";
                }
                else
                {
                    response.Errors.Add("This category already exists.");
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while adding the category.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> UpdateCategory(int id, RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                var category = await _context.CreateSet<Category>()
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

                if (category == null)
                {
                    response.Errors.Add("Category not found.");
                    return response;
                }

                // Update the category properties
                category.Name = requestCategoryDTO.Name;
                category.Description = requestCategoryDTO.Description;

                _context.CreateSet<Category>().Update(category);
                await _context.SaveChangesAsync(cancellationToken);

                response.Data = "Category updated successfully.";
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while updating the category.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<string>> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

            try
            {
                if (await IsCategoryUsedAsync(id, cancellationToken))
                {
                    response.Errors.Add("Cannot delete: Category is used in one or more movies.");
                    return response;
                }

                var category = await _context.CreateSet<Category>()
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

                if (category == null)
                {
                    response.Errors.Add("Category not found.");
                    return response;
                }

                _context.CreateSet<Category>().Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                response.Data = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while deleting the category.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<bool> IsCategoryUsedAsync(int categoryId, CancellationToken cancellationToken)
        {
            // Assumes Movie has a collection navigation property: ICollection<MovieCategory> MovieCategories
            // and MovieCategory has a CategoryId property.
            return await _context.CreateSet<Movie>()
                .AnyAsync(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId), cancellationToken);
        }
    }
}
