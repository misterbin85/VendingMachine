using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using VendineMachine.Tests.Framework;
using VendingMachine.Validators;
using Xunit;

namespace VendineMachine.Tests
{
  public class CoinsValidatorTest: IClassFixture<IocFixture>
  {
    private readonly IocFixture _fixture;

    public CoinsValidatorTest()
    {
      _fixture = new IocFixture();
    }

    [Theory]
    [InlineData("5", true)]
    [InlineData("10", true)]
    [InlineData("20", true)]
    [InlineData("50", true)]
    [InlineData("1E", true)]
    [InlineData("2E", true)]
    [InlineData("70", false)]
    [InlineData("3E", false)]
    [InlineData("5E", false)]
    public void ValidateTest(string value, bool isValid)
    {
      var validator = _fixture.IoC.Resolve<IAcceptCoinsValidator>(new TypedParameter(typeof(IList<string>), _fixture.acceptingCoins));

      //assert
      validator.Validate(value).Should().Be(isValid);
    }
  }
}
