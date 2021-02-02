
using Elsheimy.Components.ePayment.Migs.Web;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public class VpcQueryCommand : VpcAmaCommand
  {
    public override string Command => "queryDR";

    public override string EndpointUrl => "vpcdps";

    /// <summary>
    /// The unique value created by the merchant to search for.
    /// </summary>
    [QueryParam(Name = "vpc_MerchTxnRef", IsRequired = true)]
    public string MerchantTxnReference { get; set; }

    public override bool UseSecureHash => false;


    public VpcQueryCommand() : base() { }
    public VpcQueryCommand(string user, string password) : base(user, password)
    {
    }
  }
}
