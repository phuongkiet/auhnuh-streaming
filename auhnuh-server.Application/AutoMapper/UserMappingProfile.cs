using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebRequest.User;
using auhnuh_server.Domain.DTO.WebResponse.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();

            CreateMap<AddUserDTO, User>()
                .ReverseMap();

            CreateMap<UpdateUserDTO, User>()
                .ReverseMap();
        }
    }
}
