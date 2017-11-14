using FluentAssertions;
using VendineMachine.Tests.Framework;
using VendingMachine.Model;
using Xunit;

namespace VendineMachine.Tests
{
  public class VendingMachineTest: IClassFixture<IocFixture>
  {
    private readonly IocFixture _fixture;

    public VendingMachineTest()
    {
      _fixture = new IocFixture();
    }

    [Fact]
    public void ManufacturerTest()
    {
      var controller = _fixture.SetupController();

      //assert
      controller.Manufacturer.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(95, 3)]
    public void AmountTest(int cents, int euros)
    {
      _fixture.TestMoney = new Money { Cents = cents, Euros = euros };
      var controller = _fixture.SetupController();

      //assert
      controller.Amount.Should().Be(_fixture.TestMoney);
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(23, 2)]
    [InlineData(95, 3)]
    public void InsertCoinTest(int cents, int euros)
    {
      _fixture.TestMoney = default(Money);
      var controller = _fixture.SetupController();

      //assert
      controller.Amount.Should().Be(_fixture.TestMoney);
      controller.InsertCoin(new Money {Cents = cents, Euros = euros});

      //assert
      controller.Amount.Should().Be(new Money {Cents = cents, Euros = euros});
    }

    [Fact]
    public void InsertCoinCommulativeTest()
    {
      var controller = _fixture.SetupController();

      //assert
      controller.Amount.Should().Be(_fixture.TestMoney);
      controller.InsertCoin(new Money { Cents = 10, Euros = 1 });

      //assert
      controller.Amount.Should().Be(new Money { Cents = 10, Euros = 1 });
      controller.InsertCoin(new Money {Cents = 40, Euros = 2});

      //assert
      controller.Amount.Should().Be(new Money { Cents = 50, Euros = 3 });
      controller.InsertCoin(new Money { Cents = 90, Euros = 1 });

      //assert
      controller.Amount.Should().Be(new Money { Cents = 40, Euros = 5 });
    }

    [Fact]
    public void ReturnMoneyTest()
    {
      var controller = _fixture.SetupController();

      //assert
      controller.Amount.Should().Be(_fixture.TestMoney);
      controller.InsertCoin(new Money { Cents = 70, Euros = 3 });

      //assert
      controller.Amount.Should().Be(new Money { Cents = 70, Euros = 3 });

      var money = controller.ReturnMoney();

      //assert
      money.Should().Be(new Money {Cents = 70, Euros = 3});
      controller.Amount.Should().Be(default(Money));
    }

    [Fact]
    public void ProductsTest()
    {
      var controller = _fixture.SetupController();

      //assert
      controller.Products.Should().NotBeNull();

      for (int i = 0; i < _fixture.AvailableProducts.Count; i++)
      {
        //assert
        controller.Products[i].Available.Should().Be(_fixture.AvailableProducts[i].Available);
        controller.Products[i].Name.Should().Be(_fixture.AvailableProducts[i].Name);
        controller.Products[i].Price.Should().Be(_fixture.AvailableProducts[i].Price);
      }
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 1)]
    public void PositiveBuyTest(int productIndex, int availibilityAfter)
    {
      var controller = _fixture.SetupController();

      controller.InsertCoin(new Money { Cents = 30, Euros = 1 });
      var product = controller.Buy(productIndex);

      //assert
      product.Should().NotBeNull();
      controller.Products[productIndex].Available.Should().Be(availibilityAfter);
      controller.Products[productIndex].Name.Should().Be(_fixture.AvailableProducts[productIndex].Name);
      controller.Products[productIndex].Price.Should().Be(_fixture.AvailableProducts[productIndex].Price);
    }

    [Theory]    
    [InlineData(2, 1)]
    public void NegativeBuyTest(int productIndex, int availibilityAfter)
    {
      var controller = _fixture.SetupController();
      controller.InsertCoin(new Money { Cents = 30, Euros = 1 });
      var product = controller.Buy(productIndex);

      //assert
      product.Name.Should().BeNullOrEmpty();
    }
  }
}
