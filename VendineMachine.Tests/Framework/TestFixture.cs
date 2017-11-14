using System;
using System.Collections.Generic;
using Autofac;
using VendingMachine.Controller;
using VendingMachine.Model;
using VendingMachine.Reposirory;
using VendingMachine.Validators;

namespace VendineMachine.Tests.Framework
{
  public class IocFixture: IDisposable 
  {
    private static IContainer _container;

    public IocFixture()
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<VendingMachineController>().As<IVendingMachine>().InstancePerLifetimeScope();
      builder.RegisterType<MoneyRepository>().As<IMoneyRepository>().InstancePerLifetimeScope();
      builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
      builder.RegisterType<CoinsValidator>().As<IAcceptCoinsValidator>().InstancePerLifetimeScope();

      _container = builder.Build();
    }

    public void Dispose()
    {

    }

    public ILifetimeScope IoC => _container.BeginLifetimeScope();

    public IList<Product> AvailableProducts = new List<Product>
        {
            new Product() { Available = 3, Name = "Cola", Price = new Money() {Euros = 1, Cents = 25} },
            new Product() { Available = 2, Name = "Fanta", Price = new Money() {Euros = 0, Cents = 25} },
            new Product() { Available = 1, Name = "Sprite", Price = new Money() {Euros = 75, Cents = 0} }
        };

    public readonly List<string> acceptingCoins = new List<string> { "5", "10", "20", "50", "1E", "2E" };

    public Money TestMoney { get;set;}

    public IVendingMachine SetupController()
    {
      var validator = IoC.Resolve<IAcceptCoinsValidator>(new TypedParameter(typeof(string), acceptingCoins));

      var moneyRepository = IoC.Resolve<IMoneyRepository>(
          new TypedParameter(typeof(IAcceptCoinsValidator), validator),
          new TypedParameter(typeof(Money), TestMoney));

      var productRepository = IoC.Resolve<IProductRepository>(new TypedParameter(typeof(IList<Product>), AvailableProducts));

      var controller = IoC.Resolve<IVendingMachine>(
          new TypedParameter(typeof(IMoneyRepository), moneyRepository),
          new TypedParameter(typeof(IProductRepository), productRepository));

      return controller;
    }
  }
}
