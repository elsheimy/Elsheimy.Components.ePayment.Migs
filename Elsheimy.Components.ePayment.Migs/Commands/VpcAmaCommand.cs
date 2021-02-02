using Elsheimy.Components.ePayment.Migs.Web;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public abstract class VpcAmaCommand : VpcCommand
  {
    /// <summary>
    /// Command name.
    /// </summary>
    [QueryParam(Name = "vpc_User", IsRequired = true)]
    public string AMAUser { get; set; }

    /// <summary>
    /// Command name.
    /// </summary>
    [QueryParam(Name = "vpc_Password", IsRequired = true)]
    public string AMAPassword { get; set; }

    public VpcAmaCommand() : base() { }
    public VpcAmaCommand(string user, string password) : this()
    {
      this.AMAUser = user;
      this.AMAPassword = password;
    }
  }
}
