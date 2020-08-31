using System.Linq;
using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Dtos.Events;
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
            CreateMap<Event, DataForAddingEventDto>()
                .ForMember(dest => dest.Icon, opt => opt.ResolveUsing((src) => new CustomFile()));
        }
    }
}