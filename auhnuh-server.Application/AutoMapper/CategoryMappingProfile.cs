﻿using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebRequest.Category;
using auhnuh_server.Domain.DTO.WebResponse.Category;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Create Map<Request, Response>
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<RequestCategoryDTO, Category>().ReverseMap();
        }
    }
}
