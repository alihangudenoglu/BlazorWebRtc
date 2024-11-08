using AutoMapper;
using BlazorWebRtc.Application.DTO.Message;
using BlazorWebRtc.Domain;

namespace BlazorWebRtc.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<MessageRoom,MessageDto>().ReverseMap();
    }
}
