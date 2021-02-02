using Elsheimy.Components.ePayment.Migs.Web;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public class VpcServerHostedPaymentCommand : VpcPaymentCommand
  {
    public override string EndpointUrl => "vpcpay";

    /// <summary>
    /// Used in 3-party transactions to specify return URL used when the payment servers sends the response.
    /// </summary>
    [QueryParam(Name = "vpc_ReturnURL", IsRequired = true)]
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Used in 3-party transactions to specify server page locale. Default is 'en'.
    /// </summary>
    [QueryParam(Name = "vpc_Locale", IsRequired = true)]
    public string Locale { get; set; } = "en";

  }
}
