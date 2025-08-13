using System.ComponentModel.DataAnnotations.Schema;

namespace FomoGym.Models;

[Table("Staff")]
public class Staff {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Staff";
}