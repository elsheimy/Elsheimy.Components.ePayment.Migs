namespace Elsheimy.Components.ePayment.Migs.Web
{
  public class QueryParameter
  {
    public string Name { get; set; }
    public string Value { get; set; }

    public QueryParameter() { }
    public QueryParameter(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

  }
}
