using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.Reposirory
{
  public class ProductRepository : IProductRepository
  {
    public ProductRepository(IList<Product> availableProducts)
    {
      AvailableProducts = availableProducts;
    }

    public IList<Product> AvailableProducts { get; }

    public void AddProduct(int available, Money price, string name)
    {
      Product newProduct = new Product
      {
        Available = available,
        Price = price,
        Name = name
      };

      AvailableProducts.Add(newProduct);
    }

    public Product PopProduct(string name)
    {
      for (int i = 0; i < AvailableProducts.Count; i++)
      {
        var product = AvailableProducts[i];

        if (product.Available > 0 && product.Name == name)
        {
          product.Available -= 1;
          AvailableProducts.RemoveAt(i);
          AvailableProducts.Insert(i, product);
          return product;
        }
      }

      return default(Product);
    }
  }
}
