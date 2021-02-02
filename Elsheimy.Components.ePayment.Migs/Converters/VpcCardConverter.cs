
using System.Collections.Generic;

namespace Elsheimy.Components.ePayment.Migs.Converters
{
  /// <summary>
  /// Card code converter. Converts Mastercard card codes to meaningful names and vice verca. e.g. converts MC to Mastercard.
  /// </summary>
  /// <remarks>Inherit to support more cards.</remarks>
  public class VpcCardConverter : IValueConverter
  {
    protected Dictionary<string, string> Values { get; set; }

    public string this[string cardCode]
    {
      get
      {
        return this.Convert(cardCode);
      }
    }


    public VpcCardConverter()
    {
      Values = new Dictionary<string, string>();
      Values.Add("AE", VpcCardType.AmericaExpress.ToString());
      Values.Add("DC", VpcCardType.DinersClub.ToString());
      Values.Add("JC", VpcCardType.JcbCard.ToString());
      Values.Add("MC", VpcCardType.MasterCard.ToString());
      Values.Add("VC", VpcCardType.Visa.ToString());
    }

    /// <summary>
    /// Returns the meaningful card name of the provided code.
    /// </summary>
    public virtual string Convert(string cardCode)
    {
      if (cardCode == null)
        return null;

      if (Values.ContainsKey(cardCode))
        return Values[cardCode];

      return null;
    }

    /// <summary>
    /// Return the card code from the provided name.
    /// </summary>
    /// <param name="cardName"></param>
    /// <returns></returns>
    public string ConvertBack(string cardName)
    {
      if (cardName == null)
        return null;

      foreach (var val in Values)
      {
        if (val.Value == cardName)
          return val.Key;
      }

      return null;
    }
  }
}