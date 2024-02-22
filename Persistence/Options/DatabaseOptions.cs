#nullable disable

using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Persistence.Options;

public record DatabaseOptions
{
    [Required]
    public string MasterDatabaseName { get; init; }
    
    [Required]
    public string TenantDatabasePrefix { get; init; }
}