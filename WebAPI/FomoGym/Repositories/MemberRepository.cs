using FomoGym.Data;
using FomoGym.Interfaces;
using FomoGym.Models;
using Microsoft.EntityFrameworkCore;

namespace FomoGym.Repositories;

public class MemberRepository : IMemberRepository {

    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Member> CreateAsync(Member memberModel) {
        await _context.Members.AddAsync(memberModel);
        await _context.SaveChangesAsync();
        return memberModel;
    }

    public async Task<Member?> DeleteAsync(int id) {
        var memberModel = await _context.Members.FirstOrDefaultAsync(member => member.Id == id);
        if (memberModel == null) {
            return null;
        }

        _context.Members.Remove(memberModel);
        await _context.SaveChangesAsync();
        return memberModel;
    }

    public async Task<List<Member>> GetAllAsync() {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id) {
        return await _context.Members.FindAsync(id);
    }

    public async Task<Member?> UpdateAsync(int id, Member memberModel) {
        var existingMember = await _context.Members.FindAsync(id);
        if (existingMember == null) {
            return null;
        }

        existingMember.Name = memberModel.Name;
        existingMember.Email = memberModel.Email;
        existingMember.PhoneNumber = memberModel.PhoneNumber;
        existingMember.MembershipStatus = memberModel.MembershipStatus;

        await _context.SaveChangesAsync();

        return existingMember;
    }
}