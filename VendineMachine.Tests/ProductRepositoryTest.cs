using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using VendineMachine.Tests.Framework;
using VendingMachine.Model;
using VendingMachine.Reposirory;
using Xunit;

namespace VendineMachine.Tests
{
  public class ProductRepositoryTest: IClassFixture<IocFixture>
  {
    private readonly IocFixture _fixture;

    public ProductRepositoryTest()
    {
      _fixture = new IocFixture();
    }

    [Fact]
    public void AvailableproductsTest()
    {
      var listOfProducts = new TypedParameter(typeof(IList<Product>), _fixture.AvailableProducts);
      var productRepository = _fixture.IoC.Resolve<IProductRepository>(listOfProducts);

      //assert
      productRepository.AvailableProducts.Count.Should().Be(_fixture.AvailableProducts.Count);
    }

    [Theory]
    [InlineData(10, 50, 2, "Testproduct")]
    public void AddProductTest(int available, int cents, int euros, string name)
    {      
      var productRepository = _fixture.IoC.Resolve<IProductRepository>(new TypedParameter(typeof(IList<Product>), _fixture.AvailableProducts));
      productRepository.AddProduct(available, new Money() {Cents = cents, Euros = euros}, name);

      //assert
      productRepository.AvailableProducts.Count.Should().Be(_fixture.AvailableProducts.Count);
    }
  }
}
