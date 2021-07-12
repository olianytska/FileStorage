using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastrucrure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            //.ReverseMap();

            CreateMap<Directory, DirectoryDTO>();

            CreateMap<UserDirectory, DirectoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DirectoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Directory.Name))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Directory.Path))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Directory.Size))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Directory.Size))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Directory.Created))
                .ForMember(dest => dest.IsPrivate, opt => opt.MapFrom(src => src.Directory.IsPrivate))
                .ForMember(dest => dest.IsRemove, opt => opt.MapFrom(src => src.Directory.IsRemove))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));


            CreateMap<UserProfile, UserDTO>();
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName));

        }
    }
}
