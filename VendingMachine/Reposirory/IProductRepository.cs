using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.Reposirory
{
  public interface IProductRepository
  {
    IList<Product> AvailableProducts { get; }

    void AddProduct(int available, Money price, string name);

    Product PopProduct(string name);
  }
}
