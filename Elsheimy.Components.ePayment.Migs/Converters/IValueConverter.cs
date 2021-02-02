namespace Elsheimy.Components.ePayment.Migs.Converters
{
  public interface IValueConverter
  {
    string this[string key] { get; }

    string Convert(string key);
    string ConvertBack(string Value);
  }
}
