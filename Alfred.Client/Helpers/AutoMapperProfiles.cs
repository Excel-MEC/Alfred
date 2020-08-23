using Alfred.Client.Dtos.Events;
using AutoMapper;

namespace Alfred.Client.Helpers
{
    public class AutoMapperProfiles:Profile
    {

        public AutoMapperProfiles()
        {
            AllowNullDestinationValues = true;
            CreateMap<EventForDetailedViewDto, DataForAddingEventDto>()
                .ForMember(dest => dest.Icon, opt=> opt.Ignore());
        }
    }
}