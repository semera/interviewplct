namespace Api.Domain.Entities;

public record Employee : Entity
{
    public required decimal Salary { get; init; }
    public required Dependent[] Dependents { get; init; }
}
