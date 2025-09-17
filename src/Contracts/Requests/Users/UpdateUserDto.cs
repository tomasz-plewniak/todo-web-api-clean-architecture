using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Users;

public record UpdateUserDto(
    [Required]
    [Length(1, 100)]
    string UserName,
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    string Email);