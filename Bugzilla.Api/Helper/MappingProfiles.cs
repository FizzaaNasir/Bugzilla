using AutoMapper;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugzilla.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Project, ProjectDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<ProjectUser, ProjectUserDTO>();
            CreateMap<Bug, BugDTO>();

            //Needed for add operation
            CreateMap<ProjectDTO, Project>();
            CreateMap<ProjectUserDTO, ProjectUser>();
            CreateMap<BugDTO, Bug>();
            CreateMap<UserDTO, ApplicationUser>();


        }
    }
}
