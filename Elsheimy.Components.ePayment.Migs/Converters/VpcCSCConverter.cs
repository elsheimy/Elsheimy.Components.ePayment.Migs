using System;
using System.Collections.Generic;

namespace Elsheimy.Components.ePayment.Migs.Converters
{
  /// <summary>
  /// CSC Converter. Converts CSC codes to descriptions.
  /// </summary>
  /// <remarks>Inherit to support more values.</remarks>
  public class VpcCSCConverter : IValueConverter
  {
    protected Dictionary<string, string> Values { get; set; }

    /// <summary>
    /// Returns the meaningful description of the provided code.
    /// </summary>
    public string this[string code]
    {
      get
      {
        return this.Convert(code);
      }
    }

    public VpcCSCConverter()
    {
      Values = new Dictionary<string, string>();

      Values.Add("M", "Exact code match");
      Values.Add("N", "Code invalid or not matched");
      Values.Add("P", "Code not processed");
      Values.Add("S", "Merchant has indicated that CSC is not present on the card(MOTO situation)");
      Values.Add("U", "Card issuer is not registered and / or certified");
      Values.Add("Unsupported", "CSC not supported or there was no CSC data provided");

    }

    /// <summary>
    /// Returns the meaningful description of the provided code.
    /// </summary>
    public virtual string Convert(string code)
    {
      if (code == null)
        return null;

      if (Values.ContainsKey(code))
        return Values[code];

      return null;
    }

    /// <summary>
    /// Return the card code from the provided description. Not implemented.
    /// </summary>
    public string ConvertBack(string description)
    {
      throw new NotImplementedException();
    }
  }
}