using System.ComponentModel.DataAnnotations.Schema;

namespace FomoGym.Models;

[Table("Members")]
public class Member {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; }
    public string MembershipStatus { get; set; } = "Active";
}