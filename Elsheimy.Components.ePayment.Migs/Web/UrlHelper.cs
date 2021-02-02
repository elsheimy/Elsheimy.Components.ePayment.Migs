using System.Linq;

namespace Elsheimy.Components.ePayment.Migs.Web
{
  public static class UrlHelper
  {
    public static string Concat(params string[] sections)
    {
      return string.Join("/", sections.Select(a => a.Trim('/')));
    }
  }
}
