using Elsheimy.Components.ePayment.Migs.Web;
using System;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public class VpcMerchantHostedPaymentCommand : VpcPaymentCommand
  {
    public override string EndpointUrl => "vpcdps";

    /// <summary>
    /// The 16-digit card number.
    /// </summary>
    [QueryParam(Name = "vpc_CardNum", IsRequired = true)]
    public long CardNumber { get; set; }

    /// <summary>
    /// Card expiry date as number in yyMM format. Use CardExpiryYear and CardExpiryMonth to change value.
    /// </summary>
    [QueryParam(Name = "vpc_CardExp", IsRequired = true)]
    public int CardExp
    {
      get
      {
        var date = CardExpiryDate;

        if (null == date)
          return 0;

        return int.Parse(date.Value.ToString("yyMM", System.Globalization.CultureInfo.InvariantCulture));
      }
    }
    /// <summary>
    /// Card expiry date. Use CardExpiryYear and CardExpiryMonth to change value.
    /// </summary>
    public DateTime? CardExpiryDate
    {
      get
      {
        try
        {
          return new DateTime(CardExpiryYear, CardExpiryMonth, 1);
        }
        catch (ArgumentOutOfRangeException)
        {
          return null;
        }
      }
    }
    /// <summary>
    /// Card expiry year.
    /// </summary>
    public int CardExpiryYear { get; set; }
    /// <summary>
    /// Card expiry month.
    /// </summary>
    public int CardExpiryMonth { get; set; }

    /// <summary>
    /// Card security code.
    /// </summary>
    [QueryParam(Name = "vpc_CardSecurityCode", IsRequired = false)]
    public int CardSecurityCode { get; set; }
  }
}
