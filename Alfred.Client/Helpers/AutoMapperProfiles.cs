using System.Linq;
using Alfred.Client.Dtos.Admin;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Extensions;
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
        }
    }
}