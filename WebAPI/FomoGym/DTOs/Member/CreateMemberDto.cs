using System.ComponentModel.DataAnnotations;
namespace FomoGym.DTOs.Member;

public class CreateMemberDto {
    [Required]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}