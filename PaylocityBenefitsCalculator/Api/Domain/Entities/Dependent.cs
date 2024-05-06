namespace Api.Domain.Entities;

public record Dependent : Entity
{
    public required Relationship Relationship { get; init; }
}
