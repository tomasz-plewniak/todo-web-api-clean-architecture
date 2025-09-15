using System.ComponentModel.DataAnnotations;

namespace Application.Users;

public record CreateUser(
    [Required]
    [Length(1, 100)]
    string UserName,
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    string Email);