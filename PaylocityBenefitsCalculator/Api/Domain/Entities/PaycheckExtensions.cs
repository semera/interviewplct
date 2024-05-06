namespace Api.Domain.Entities;

public static class PaycheckExtensions
{
    public static PaycheckItem AddItem(this Paycheck paycheck, string name, int days, decimal dailyRate)
    {
        var item = new PaycheckItem
        {
            Name = name,
            Days = days,
            DailyRate = dailyRate,
        };

        paycheck.Items.Add(item);
        paycheck.NetSalary += item.Salary;
        return item;
    }
}
