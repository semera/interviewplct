using System;
using System.Collections.Generic;
using Api.Domain.Entities;
using Bogus;

namespace ApiTests;

public class EmployeeBuilder
{
    private int _id = 0;
    private DateTime _birthDay = DateTime.Now.Date.AddYears(-20);
    private decimal _salary = 81234;
    private readonly Faker _faker = new();
    private readonly List<Dependent> _dependents = [];

    public EmployeeBuilder WithBirthday(DateTime birthDay)
    {
        _birthDay = birthDay.Date;
        return this;
    }

    public EmployeeBuilder WithSalary(decimal salary)
    {
        _salary = salary;
        return this;
    }

    public EmployeeBuilder WithDependent(Relationship? relationship = null, DateTime? birthday = null)
    {
        Dependent d = new()
        {
            Id = _id++,
            DateOfBirth = birthday ?? _faker.Date.Past(20, _birthDay.AddYears(-20)).Date,
            Relationship = relationship ?? Relationship.Child,
            FirstName = _faker.Name.FirstName(),
            LastName = _faker.Name.LastName(),
        };

        _dependents.Add(d);
        return this;
    }

    public Employee Build()
    {
        return new Employee()
        {
            DateOfBirth = _birthDay,
            Salary = _salary,
            FirstName = _faker.Name.FirstName(),
            LastName = _faker.Name.LastName(),
            Id = _id++,
            Dependents = [.. _dependents]
        };
    }
}
