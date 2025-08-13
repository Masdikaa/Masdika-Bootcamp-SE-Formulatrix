using FomoGym.Models;

namespace FomoGym.Interfaces;

public interface IMemberRepository {
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> CreateAsync(Member memberModel);
    Task<Member?> UpdateAsync(int id, Member memberModel);
    Task<Member?> DeleteAsync(int id);
}