using FomoGym.DTOs.Member;
using FomoGym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FomoGym.Controllers;

[Route("fomogym/members")]
[ApiController]
public class MembersController : ControllerBase {
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService) {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var members = await _memberService.GetAllAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var member = await _memberService.GetByIdAsync(id);
        if (member == null) {
            return NotFound();
        }
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberDto memberDto) {
        var newMember = await _memberService.CreateAsync(memberDto);
        if (newMember == null) {
            return BadRequest("Could not create member.");
        }
        return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateMemberDto memberDto) {
        var updatedMember = await _memberService.UpdateAsync(id, memberDto);
        if (updatedMember == null) {
            return NotFound();
        }
        return Ok(updatedMember);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var deletedMember = await _memberService.DeleteAsync(id);
        if (deletedMember == null) {
            return NotFound();
        }
        return NoContent();
    }

}