#nullable disable

using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Persistence.Options;

public record ConnectionStringOptions
{
    [Required]
    public string Host { get; init; }

    [Required]
    public string Port { get; init; }

    [Required]
    public string Username { get; init; }

    [Required]
    public string Password { get; init; }
}