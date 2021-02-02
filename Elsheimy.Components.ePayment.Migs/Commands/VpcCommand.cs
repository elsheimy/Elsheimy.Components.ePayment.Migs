using Elsheimy.Components.ePayment.Migs.Web;
using System.Collections.Generic;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public abstract class VpcCommand
  {
    /// <summary>
    /// Command name.
    /// </summary>
    [QueryParam(Name = "vpc_Command", IsRequired = true)]
    public abstract string Command { get; }
    /// <summary>
    /// Command endpoint URL.
    /// </summary>
    public abstract string EndpointUrl { get; }

    public virtual bool UseSecureHash { get { return true; } }

    /// <summary>
    /// Returns a list of query parameters of the current object.
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<QueryParameter> GetQueryParams()
    {
      return QueryManager.GenerateQueryParameters(this);
    }
  }
}
