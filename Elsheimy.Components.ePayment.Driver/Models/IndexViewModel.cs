using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elsheimy.Components.ePayment.Driver.Models
{
  public class IndexViewModel
  {
    public string MerchantID { get; set; }
    public string AccessCode { get; set; }
    public string HashSecret { get; set; }

    public string TransactionReference { get; set; }
    public string OrderInfo { get; set; }
    public decimal Amount { get; set; }

    public string Locale { get; set; }
    public long CardNumber { get; set; }
    public int CardExpiryYear { get; set; }
    public int CardExpiryMonth { get; set; }
    public int CardSecurityCode { get;  set; }
  }
}
