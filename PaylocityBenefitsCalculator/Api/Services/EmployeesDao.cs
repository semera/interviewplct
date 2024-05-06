using Api.Domain.Entities;

namespace Api.Services;


// TODO: 1. for simplicity DAO is used to access the data, there is no real database connection, no transaction handling ...
// TODO: 2. with limited time I don't have created separated entities for domain and for persistence.
// TODO: 3. please ignore this O(N) implementation of searching, it's just for testing purposes
public class EmployeesDao : IEmployeesDao
{
    private readonly List<Employee> _employees;

    public EmployeesDao()
    {
        // mocked data
        _employees =
        [
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30),
                Dependents = []
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents =
                [
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                ]
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents =
                [
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                ]
            }
        ];

    }

    public Task<List<Dependent>> GetAllDependent() => Task.FromResult(_employees.SelectMany(x => x.Dependents).ToList());
    public Task<List<Employee>> GetAllEmployees() => Task.FromResult(_employees);
    public Task<Dependent?> GetDependent(int id) => Task.FromResult(_employees.SelectMany(x => x.Dependents).FirstOrDefault(x => x.Id == id));
    public Task<Employee?> GetEmployee(int id) => Task.FromResult(_employees.FirstOrDefault(x => x.Id == id));
}
