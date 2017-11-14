using System.Configuration;
using System.Linq;
using VendingMachine.Model;
using VendingMachine.Reposirory;

namespace VendingMachine.Controller
{
  public class VendingMachineController: IVendingMachine
  {
    private readonly IMoneyRepository _moneyRepository;
    private readonly IProductRepository _productRepository;
    private const string _manufacturer = "default Manufacturer";

    public VendingMachineController(IMoneyRepository moneyRepository, IProductRepository productRepository)
    {
      _moneyRepository = moneyRepository;
      _productRepository = productRepository;
    }

    public string Manufacturer
    {
      get
      {
        var manufacturer = ConfigurationManager.AppSettings["Manufacturer"];
        return string.IsNullOrEmpty(manufacturer) ? _manufacturer : manufacturer;
      }
    }

    public Money Amount => _moneyRepository.TotalMoney;

    public Money InsertCoin(Money amount)
    {
      return _moneyRepository.IncertCoins(amount);
    }

    public Money ReturnMoney()
    {      
      return _moneyRepository.ReturnMoney();
    }

    public Product[] Products
    {
      get { return _productRepository.AvailableProducts.ToArray(); }
      set
      {
        foreach (var product in value)
        {
          _productRepository.AddProduct(product.Available, product.Price, product.Name);
        }
      }
    }

    public Product Buy(int productNumber)
    {
      var desiredProduct = _productRepository.AvailableProducts[productNumber];

      if (_moneyRepository.AmountInCents(desiredProduct.Price) <=
          _moneyRepository.AmountInCents(_moneyRepository.TotalMoney))
      {
        _moneyRepository.UpdateReminder(desiredProduct.Price);

        return _productRepository.PopProduct(desiredProduct.Name);
      }

      return default(Product);
    }
  }
}
