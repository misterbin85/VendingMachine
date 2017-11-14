using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Validators
{
  public class CoinsValidator: IAcceptCoinsValidator
  {
    private readonly IList<string> _acceptingCoins;

    public CoinsValidator(IList<string> acceptingCoins)
    {
      _acceptingCoins = acceptingCoins;
    }

    public bool Validate(string valueToValidate)
    {
      return _acceptingCoins.Any(s => s.Equals(valueToValidate));
    }
  }
}
