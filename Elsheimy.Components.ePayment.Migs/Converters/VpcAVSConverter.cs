
using System;
using System.Collections.Generic;

namespace Elsheimy.Components.ePayment.Migs.Converters
{
  /// <summary>
  /// AVS Converter. Converts AVS codes to descriptions.
  /// </summary>
  /// <remarks>Inherit to support more values.</remarks>
  public class VpcAVSConverter : IValueConverter
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

    public VpcAVSConverter()
    {
      Values = new Dictionary<string, string>();

      Values.Add("0", "AVS not requested");
      Values.Add("A", "Address match only");
      Values.Add("B", "Street Address match for international transaction. Postal Code not verified due to incompatible formats.");
      Values.Add("C", "Street Address and Postal Code not verified for International Transaction due to incompatible formats.");
      Values.Add("D", "Street Address and postal code match for international transaction.");
      Values.Add("E", "Address and ZIP / postal code not provided");
      Values.Add("F", "Street address and postal code match.Applies to U.K.only.");
      Values.Add("G", "Issuer does not participate in AVS(international transaction)");
      Values.Add("I", "Visa Only.Address information not verified for international transaction");
      Values.Add("K", "Card holder name only matches.");
      Values.Add("M", "Street Address and postal code match for international transaction.");
      Values.Add("N", "Address and ZIP / postal code not matched.");
      Values.Add("P", "Postal Codes match for international transaction but street address not verified due to incompatible formats.");
      Values.Add("R", "Issuer system is unavailable.");
      Values.Add("S", "Service not supported or address not verified(international transaction).");
      Values.Add("U", "Address unavailable or not verified");
      Values.Add("Unsupported", "Unsupported");
      Values.Add("W", "9 digit ZIP / postal code matched, Address not Matched");
      Values.Add("Y", "Exact match - address and 5 digit ZIP / postal code");
      Values.Add("Z", "5 digit ZIP / postal code matched, Address not Matched");

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