# Implementation Notes and Decisions

## Focus

- The majority of time was spent on domain modeling.
  - This should make it easy to change or add new features.
  - The best starting point to understand the code is [PaycheckService](/PaylocityBenefitsCalculator/Api/Domain/Paychecks/PaycheckService.cs).

## Requirement Implementation

- There are two different approaches for dividing the year into 26 pieces.
  - The first approach starts on New Year's Day, where every period has 14 days except the first period which has 15 days (and the second in a leap year).
  - The second approach ensures every period has exactly 14 days, starting on Monday and ending on Sunday. This starts on the first Monday of the year.
  - The second approach is more natural, but not exactly what was required in the task.

- All calculations are based on a one-day rate, then multiplied by the number of days in the period.
  - This means weekends are treated as normal days.
  - This is not a real-time approach, but it can be easily updated with a business calendar, holiday planner, and so on.
  - Dependents are handled from a date when they started to be valid, for example child could be counted on for several days if he was born during the period.
  - All day rates are rounded to 2 decimal places, so there may be some rounding errors.

### Example:
![image](https://github.com/semera/interviewplct/assets/10085862/676fca9e-e571-48bb-8bea-3646682cef4f)


- Paychecks are implemented as a sub-resource of employees in the EmployeesController.
  - The controller directly calls IPaycheckService from the domain. This approach calculates the paycheck directly on the Web Server. It would be better to move the calculation to a service behind a request/reply (either async messaging or at least a gRPC call).

## Test Coverage

- There is almost full coverage for the domain.
- The rest of the tests are minimal.

## Integration Testing

- Integrated tests are switched to 'Microsoft.AspNetCore.Mvc.Testing'.
- It's possible to switch and test the real API with the environment variable:

```
USE_REAL_SERVER=true
```

## Data Access

- DAO was used for simplicity.
- Due to time constraints, there are no separate objects for the data layer and business layer.

## Validations

- There is no real input data validation.
- Only validation of data before use during processing has been implemented.
- I consider this situation "inconsistent data" in the database, like an assert exception. It throws an exception and generates an internal server error.

## Unhandled Exceptions

- These are handled with middleware.
- As mentioned above, no validation or other exceptions are handled.

## TODO:

- There are many places in the code to discuss - these are marked with TODO:

