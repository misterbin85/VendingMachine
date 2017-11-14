using VendingMachine.Model;
using VendingMachine.Validators;

namespace VendingMachine.Reposirory
{
  public class MoneyRepository: IMoneyRepository
  {
    private int _totalMoney;
    private readonly IAcceptCoinsValidator _validator;

    private Money MoneyToCent
    {
      get
      {
        int totalEuros = _totalMoney / 100;
        int totalCents = _totalMoney % 100;
        return new Money { Euros = totalEuros, Cents = totalCents };
      }
    }
    public MoneyRepository(IAcceptCoinsValidator validator, Money totalMoney)
    {
      _validator = validator;
      _totalMoney = AmountInCents(totalMoney);
    }
    public Money TotalMoney => MoneyToCent;
    public Money IncertCoins(Money amount)
    {
      var incerted = AmountInCents(amount);
      _totalMoney += incerted;
      return MoneyToCent;
    }
    public Money UpdateReminder(Money ammount)
    {
      var priceInCent = AmountInCents(ammount);
      _totalMoney = _totalMoney - priceInCent;
      if (_totalMoney <= 0)
      {
        return default(Money);
      }
      return MoneyToCent;
    }
    public int AmountInCents(Money money)
    {
      return money.Euros * 100 + money.Cents;
    }
    public Money ReturnMoney()
    {
      var money = MoneyToCent;
      _totalMoney = 0;
      return money;
    }
    public bool Validate(string money)
    {
      return _validator.Validate(money);
    }
  }
}
