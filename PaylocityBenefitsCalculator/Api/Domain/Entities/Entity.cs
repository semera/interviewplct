﻿using System;

namespace Api.Domain.Entities;

public abstract record Entity
{
    public required int Id { get; init; }
    public required string? FirstName { get; init; }
    public required string? LastName { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
