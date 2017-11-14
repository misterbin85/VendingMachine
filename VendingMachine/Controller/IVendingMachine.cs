using VendingMachine.Model;

namespace VendingMachine.Controller
{
  public interface IVendingMachine
  {
    string Manufacturer { get; }

    Money Amount { get; }

    Money InsertCoin(Money amount);

    Money ReturnMoney();

    Product[] Products { get; set; }

    Product Buy(int productNumber);
  }
}
