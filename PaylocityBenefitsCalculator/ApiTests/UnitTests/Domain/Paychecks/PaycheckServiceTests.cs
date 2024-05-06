using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Paychecks;
using Api.Domain.Segmenting;
using Moq;
using Api.Domain.Entities;
using Api.Domain.Validations;

namespace ApiTests.UnitTests.Domain.Paychecks;

public class PaycheckServiceTests
{
    [Fact]
    public void GetPaychecks_ValidInput_26CalculationsReturns26Paychecks()
    {
        // Arrange
        Mock<IPaycheckCalculator> paycheckCalculatorMock = new();
        paycheckCalculatorMock
            .Setup(x => x.Calculate(It.IsAny<PayPeriod>(), It.IsAny<Employee>()))
            .Returns(new Paycheck { AnnualSalary = 0, Period = new PayPeriod { EndDate = DateTime.Now, StartDate = DateTime.Now } }); 

        // TODO: use moq intead exact type  
        FullYearPayPeriodSegmenter payPeriodSegmenter = new();

        PaycheckService paycheckService = new(paycheckCalculatorMock.Object, payPeriodSegmenter, Array.Empty<IValidation>());

        Employee employee = new EmployeeBuilder().Build();
        int year = 2022; 

        // Act
        Paycheck[] result = paycheckService.GetPaychecks(employee, year);

        // Assert
        Assert.Equal(26, result.Length);
        paycheckCalculatorMock.Verify(x => x.Calculate(It.IsAny<PayPeriod>(), It.IsAny<Employee>()), Times.Exactly(26));
    }

    [Fact]
    public void GetPaychecks_InvalidData_Throws()
    {
        // Arrange
        Mock<IPaycheckCalculator> paycheckCalculatorMock = new();
        paycheckCalculatorMock
            .Setup(x => x.Calculate(It.IsAny<PayPeriod>(), It.IsAny<Employee>()))
            .Returns(new Paycheck { AnnualSalary = 0, Period = new PayPeriod { EndDate = DateTime.Now, StartDate = DateTime.Now } });

        // TODO: use moq intead exact type  
        FullYearPayPeriodSegmenter payPeriodSegmenter = new();
        IValidation[] validations = [new EmployeeCanHaveOnlyOnePartnerValidation()]; // test in isolation
        
        PaycheckService paycheckService = new(paycheckCalculatorMock.Object, payPeriodSegmenter, validations);

        Employee employee = new EmployeeBuilder()
            .WithDependent(relationship: Relationship.DomesticPartner)
            .WithDependent(relationship: Relationship.Spouse)
            .Build();

        int year = 2022;

        // Act
        Action act = () => paycheckService.GetPaychecks(employee, year);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
