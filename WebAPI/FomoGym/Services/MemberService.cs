using AutoMapper;
using FomoGym.DTOs.Member;
using FomoGym.Interfaces;
using FomoGym.Models;

namespace FomoGym.Services;

public class MemberService : IMemberService {
    private readonly IMemberRepository _memberRepo;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository memberRepo, IMapper mapper) {
        _memberRepo = memberRepo;
        _mapper = mapper;
    }

    public async Task<MemberDto?> CreateAsync(CreateMemberDto memberDto) {
        var memberModel = _mapper.Map<Member>(memberDto);
        await _memberRepo.CreateAsync(memberModel);
        return _mapper.Map<MemberDto>(memberModel);
    }

    public async Task<MemberDto?> DeleteAsync(int id) {
        var memberModel = await _memberRepo.DeleteAsync(id);
        if (memberModel == null) {
            return null;
        }
        return _mapper.Map<MemberDto>(memberModel);
    }

    public async Task<List<MemberDto>> GetAllAsync() {
        var members = await _memberRepo.GetAllAsync();
        return _mapper.Map<List<MemberDto>>(members);
    }

    public async Task<MemberDto?> GetByIdAsync(int id) {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) {
            return null;
        }
        return _mapper.Map<MemberDto>(member);
    }

    public async Task<MemberDto?> UpdateAsync(int id, CreateMemberDto memberDto) {
        var memberModel = _mapper.Map<Member>(memberDto);
        var updatedMember = await _memberRepo.UpdateAsync(id, memberModel);
        if (updatedMember == null) {
            return null;
        }
        return _mapper.Map<MemberDto>(updatedMember);
    }
}
