using VendingMachine.Model;

namespace VendingMachine.Reposirory
{
  public interface IMoneyRepository
  {
    Money TotalMoney { get; }
    Money IncertCoins(Money amount);
    Money UpdateReminder(Money ammount);
    int AmountInCents(Money money);
    Money ReturnMoney();
    bool Validate(string money);
  }
}
