using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Elsheimy.Components.ePayment.Migs.Web
{
  public static class QueryManager
  {
    /// <summary>
    /// Checks target object properties/fields for existence QueryParamAttribute and returns a dictionary of query parameters.
    /// </summary>
    public static IEnumerable<QueryParameter> GenerateQueryParameters(object targetObject)
    {
      IEnumerable<MemberInfo> queryMembers = GetObjectQueryMembers(targetObject);

      List<QueryParameter> queryParams = new List<QueryParameter>(queryMembers.Count());

      foreach (var member in queryMembers)
      {
        // Gets QueryParamAttribute attribute
        var queryAtt = member.GetCustomAttributes<QueryParamAttribute>().FirstOrDefault();
        if (null == queryAtt)
          continue;

        object value = null;
        if (member is PropertyInfo)
          value = (member as PropertyInfo).GetValue(targetObject);
        else // member is a field
          value = (member as FieldInfo).GetValue(targetObject);

        string name = System.Web.HttpUtility.UrlEncode(queryAtt.Name ?? member.Name);
        string valueStr = value != null ? value.ToString() : string.Empty;


        if (valueStr == string.Empty && queryAtt.IsRequired == false)
          continue;

        QueryParameter param = new QueryParameter();
        param.Name = name;
        param.Value = valueStr;

        queryParams.Add(param);
      }

      return queryParams;
    }
    
    /// <summary>
    /// Returns a list of QueryParameter from the specified query string.
    /// </summary>
    /// <param name="queryStr"></param>
    /// <returns></returns>
    public static IEnumerable<QueryParameter> ExtractQueryParameters(string queryStr)
    {
      NameValueCollection coll = HttpUtility.ParseQueryString(queryStr);
      List<QueryParameter> queryParams = new List<QueryParameter>(coll.Count);

      foreach (var key in coll.AllKeys)
        queryParams.Add(new QueryParameter(key, coll[key]));

      return queryParams;

    }
    private static IEnumerable<MemberInfo> GetObjectQueryMembers(object targetObject)
    {
      IEnumerable<MemberInfo> queryMembers;

      // Loads for instance properties
      queryMembers = targetObject.GetType().GetProperties();
      // Loads for instance fields
      queryMembers = queryMembers.Concat(targetObject.GetType().GetFields());

      // Checks QueryParamAttribute existence
      queryMembers = queryMembers.Where(a => a.GetCustomAttributes<QueryParamAttribute>().Any());
      return queryMembers;
    }

    /// <summary>
    /// Apply values of the given QueryParameters to the target object.
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="parameters"></param>
    public static void ApplyQueryParameters(object targetObject, IEnumerable<QueryParameter> parameters)
    {
      IEnumerable<MemberInfo> queryMembers = GetObjectQueryMembers(targetObject);
      Func<MemberInfo, string, bool> isValidMember = (mem, name) =>
      {
        var att = mem.GetCustomAttributes<QueryParamAttribute>().FirstOrDefault();
        return (att.Name == null && mem.Name == name) || (att.Name == name);
      };

      foreach (var param in parameters)
      {
        var validMembers = queryMembers.Where(a => isValidMember(a, param.Name));

        foreach (var member in validMembers)
        {
          if (member is PropertyInfo)
          {
            PropertyInfo prop = (member as PropertyInfo);
            prop.SetValue(targetObject, Convert.ChangeType(param.Value, prop.PropertyType));
          }
          else
          {
            FieldInfo field = (member as FieldInfo);
            field.SetValue(targetObject, Convert.ChangeType(param.Value, field.FieldType));
          }
        }
      }
    }

    /// <summary>
    /// Creates a query string from QueryParameter list.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static string CreateQueryString(IEnumerable<QueryParameter> parameters)
    {
      string queryStr = string.Empty;
      foreach (var param in parameters)
      {
        queryStr += string.Format("{0}={1}&", param.Name, param.Value);
      }

      queryStr = queryStr.TrimEnd('&');
      return queryStr;
    }
  }
}
