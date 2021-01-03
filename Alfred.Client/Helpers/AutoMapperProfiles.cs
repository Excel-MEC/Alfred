using System.Linq;
using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Dtos.Results;
using Alfred.Client.Extensions;
using Alfred.Client.Models;
using Alfred.Client.Models.Components;
using AutoMapper;

namespace Alfred.Client.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            AllowNullDestinationValues = true;
            CreateMap<EventForDetailedViewDto, DataForAddingEventDto>()
                .ForMember(dest => dest.Icon, opt => opt.Ignore());
            CreateMap<UserForListViewDto, StaffForListViewDto>()
                .ForMember(dest => dest.Role, opt => opt.ResolveUsing(src => src.Role.Split(",")));
            CreateMap<Event, EventForListViewDto>();
            CreateMap<ResultsForViewDto, Result>();
            CreateMap<Result, ResultsForViewDto>();
            CreateMap<Event, DataForAddingEventDto>()
                .ForMember(dest => dest.Icon, opt => opt.ResolveUsing((src) => new CustomFile()));
            CreateMap<RegistrationFromRepoDto, RegistrationForViewDto>()
                .ForMember(dest => dest.TeamName, opt => opt.ResolveUsing(src => src.Team?.Name))
                .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.User.Name))
                .ForMember(dest => dest.Email, opt => opt.ResolveUsing(src => src.User.Email))
                .ForMember(dest => dest.Role, opt => opt.ResolveUsing(src => src.User.Role))
                .ForMember(dest => dest.Picture, opt => opt.ResolveUsing(src => src.User.Picture))
                .ForMember(dest => dest.QRCodeUrl, opt => opt.ResolveUsing(src => src.User.QRCodeUrl))
                .ForMember(dest => dest.Gender, opt => opt.ResolveUsing(src => src.User.Gender))
                .ForMember(dest => dest.MobileNumber, opt => opt.ResolveUsing(src => src.User.MobileNumber))
                .ForMember(dest => dest.Category, opt => opt.ResolveUsing(src => src.User.Category))
                .ForMember(dest => dest.IsPaid, opt => opt.ResolveUsing(src => src.User.IsPaid));
            CreateMap<RegistrationForViewDto, UserForListViewDto>()
                .ForMember(dest => dest.Id, opt => opt.ResolveUsing(src => src.ExcelId));
        }
    }
}