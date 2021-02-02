
using Elsheimy.Components.ePayment.Migs.Web;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public abstract class VpcPaymentCommand : VpcCommand
  {
    public override string Command => "pay";

    /// <summary>
    /// A unique value created by the merchant to identify the transaction request.
    /// </summary>
    [QueryParam(Name = "vpc_MerchTxnRef", IsRequired = true)]
    public string MerchantTxnReference { get; set; }

    /// <summary>
    /// Your own identifier used to identify the transaaction with the cardholder. e.g. shopping card number..
    /// </summary>
    [QueryParam(Name = "vpc_OrderInfo", IsRequired = true)]
    public string OrderInfo { get; set; }

    /// <summary>
    /// The amount of transaction in the smallest currency unit. e.g. if the transaction amount is $49.95 then the amount in cents is 4995.
    /// </summary>
    [QueryParam(Name = "vpc_Amount", IsRequired = true)]
    public int Amount { get { return (int)(ActualAmount * 100.0M); } }

    /// <summary>
    /// The actual amount of transaction.
    /// </summary>
    public decimal ActualAmount { get; set; }

    /// <summary>
    /// Allows you to include a ticket number, such as an airline ticket number.
    /// </summary>
    /// <remarks>Not returned in response.</remarks>
    [QueryParam(Name = "vpc_TicketNo", IsRequired = false)]
    public string TicketNo { get; set; }


  }
}
