
using System;
using System.Collections.Generic;

namespace Elsheimy.Components.ePayment.Migs.Converters
{
  /// <summary>
  /// Response Converter. Converts Response codes to descriptions.
  /// </summary>
  /// <remarks>Inherit to support more values.</remarks>
  public class VpcResponseConverter : IValueConverter
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

    public VpcResponseConverter()
    {
      Values = new Dictionary<string, string>();

      Values.Add("?", "Transaction status is unknown");
      Values.Add("0", "Transaction Successful");
      Values.Add("1", "Transaction Declined");
      Values.Add("2", "Bank Declined Transaction");
      Values.Add("3", "No Reply from Bank");
      Values.Add("4", "Expired Card");
      Values.Add("5", "Insufficient Funds");
      Values.Add("6", "Error Communicating with Bank");
      Values.Add("7", "Payment Server detected an error");
      Values.Add("8", "Transaction Type Not Supported");
      Values.Add("9", "Bank declined transaction(Do not contact Bank)");
      Values.Add("A", "Transaction Aborted");
      Values.Add("B", "Transaction Declined -Contact the Bank");
      Values.Add("C", "Transaction Cancelled");
      Values.Add("D", "Deferred transaction has been received and is awaiting processing");
      Values.Add("E", "Transaction Declined - Refer to card issuer");
      Values.Add("F", "3 - D Secure Authentication failed");
      Values.Add("I", "Card Security Code verification failed");
      Values.Add("L", "Shopping Transaction Locked(Please try the transaction again later)");
      Values.Add("M", "Transaction Submitted(No response from acquirer)");
      Values.Add("N", "Cardholder is not enrolled in Authentication scheme");
      Values.Add("P", "Transaction has been received by the Payment Adaptor and is being processed");
      Values.Add("R", "Transaction was not processed - Reached limit of retry attempts allowed");
      Values.Add("S", "Duplicate SessionID");
      Values.Add("T", "Address Verification Failed");
      Values.Add("U", "Card Security Code Failed");
      Values.Add("V", "Address Verification and Card Security Code Failed");


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