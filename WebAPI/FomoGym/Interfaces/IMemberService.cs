using FomoGym.DTOs.Member;
using FomoGym.Models;

namespace FomoGym.Interfaces;

public interface IMemberService {
    Task<List<MemberDto>> GetAllAsync();
    Task<MemberDto?> GetByIdAsync(int id);
    Task<MemberDto?> CreateAsync(CreateMemberDto memberDto);
    Task<MemberDto?> UpdateAsync(int id, CreateMemberDto memberDto);
    Task<MemberDto?> DeleteAsync(int id);
}