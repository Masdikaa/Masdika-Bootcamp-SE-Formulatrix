using AutoMapper;
using FomoGym.DTOs.Member;
using FomoGym.Models;

namespace FomoGym.Mappers;

public class MemberProfile : Profile {
    public MemberProfile() {
        CreateMap<Member, MemberDto>();
        CreateMap<CreateMemberDto, Member>();
    }
}