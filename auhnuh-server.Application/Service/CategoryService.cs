using auhnuh_server.Application.IRepository;
using auhnuh_server.Application.IService;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Category;
using auhnuh_server.Domain.DTO.WebResponse.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.Service
{
    [AutoRegister]
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponseModel<List<CategoryDTO>>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<ApiResponseModel<CategoryDTO>> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<ApiResponseModel<string>> AddCategory(RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            return await _categoryRepository.AddCategory(requestCategoryDTO, cancellationToken);
        }

        public async Task<ApiResponseModel<string>> UpdateCategory(int id, RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            return await _categoryRepository.UpdateCategory(id, requestCategoryDTO, cancellationToken);
        }

        public async Task<ApiResponseModel<string>> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.DeleteCategory(id, cancellationToken);
        }
    }
}
