namespace VendingMachine.Validators
{
  public interface IAcceptCoinsValidator
  {
    bool Validate(string valueToValidate);
  }
}
