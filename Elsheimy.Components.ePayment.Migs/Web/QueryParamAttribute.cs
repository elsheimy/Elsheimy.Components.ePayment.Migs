using System;

namespace Elsheimy.Components.ePayment.Migs.Web
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class QueryParamAttribute : Attribute
  {
    /// <summary>
    /// Target parameter name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Is parameter required. Indicates whether to include an empty string if parameter value is null/empty.  Default is True.
    /// </summary>
    public bool IsRequired { get; set; }
  }
}
