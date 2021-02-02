using Elsheimy.Components.ePayment.Migs.Commands;
using Elsheimy.Components.ePayment.Migs.Security;
using Elsheimy.Components.ePayment.Migs.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Elsheimy.Components.ePayment.Migs
{
  public class VpcClient
  {
    #region Authentication
    /// <summary>
    /// Used for authenticating the merchant on VPC server.
    /// </summary>
    [QueryParam(Name = "vpc_AccessCode", IsRequired = true)]
    public string AccessCode { get; set; }
    /// <summary>
    /// Unique merchant ID.
    /// </summary>
    [QueryParam(Name = "vpc_Merchant", IsRequired = true)]
    public string MerchantID { get; set; }
    /// <summary>
    /// Used for integrity checking.
    /// </summary>
    public string HashSecret { get; set; }
    #endregion


    #region VPC Params
    /// <summary>
    /// The version of CommWeb VPC API used.
    /// </summary>
    [QueryParam(Name = "vpc_Version", IsRequired = true)]
    public int Version { get; set; } = 1;

    /// <summary>
    /// Hash algorithm to be used when generating secure hashes.
    /// </summary>
    public VpcSecureHashType SecureHashType { get; set; } = VpcSecureHashType.SHA256;
    public string SecureHashTypeParamName { get { return "vpc_SecureHashType"; } }
    public string SecureHashParamName { get { return "vpc_SecureHash"; } }
    #endregion

    public virtual string BaseUrl { get { return "https://migs.mastercard.com.au/"; } }
    public SecureHashService SecureHashService { get; set; }


    public VpcClient()
    {
      SecureHashService = new SecureHashService();
    }
    public VpcClient(string merchantID, string accessCode, string hashSecret) : this()
    {
      this.MerchantID = merchantID;
      this.AccessCode = accessCode;
      this.HashSecret = hashSecret;
    }


    #region Query Params
    /// <summary>
    /// Returns a list of query parameters of the current object.
    /// </summary>
    public virtual IEnumerable<QueryParameter> GetQueryParameters()
    {
      return QueryManager.GenerateQueryParameters(this);
    }

    /// <summary>
    /// Returns a list of query parameters of the current object combined with the query parameters of the given command.
    /// </summary>
    public virtual IEnumerable<QueryParameter> GetQueryParameters(VpcCommand cmd)
    {
      return GetQueryParameters().Concat(cmd.GetQueryParams());
    }
    #endregion

    #region URL
    /// <summary>
    /// Returns the full endpoint URL of the current command.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public virtual string GetUrl(VpcCommand cmd)
    {
      return UrlHelper.Concat(this.BaseUrl, cmd.EndpointUrl);
    }
    #endregion

    /// <summary>
    /// Generates callable URL of the given command.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public virtual string ComputeCommand(VpcCommand cmd)
    {
      // command url
      string url = GetUrl(cmd);


      // query parameters
      var queryParams = GetQueryParameters(cmd);
      // ASCII order as required by VPC
      queryParams = queryParams.OrderBy(a => a.Name, StringComparer.Ordinal);

      string queryStr = QueryManager.CreateQueryString(queryParams);
      string secureHashQueryStr = GenerateSecureHashQueryString(cmd, queryParams); ;

      return string.Concat(url, "?", queryStr, "&", secureHashQueryStr);
    }

    private string GenerateSecureHashQueryString(VpcCommand cmd, IEnumerable<QueryParameter> queryParams)
    {
      if (cmd.UseSecureHash)
      {
        // secure hash extra params
        List<QueryParameter> secureHashParams = new List<QueryParameter>(2);
        string secureHash = SecureHashService.GenerateSecureHash(this.HashSecret, queryParams, SecureHashType);
        secureHashParams.Add(new QueryParameter(SecureHashParamName, secureHash));
        secureHashParams.Add(new QueryParameter(SecureHashTypeParamName, SecureHashType.ToString()));
        return QueryManager.CreateQueryString(secureHashParams);
      }

      return string.Empty;
    }

    /// <summary>
    /// Executes the given command and returns the response body as string.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public virtual string ExecuteCommandRaw(VpcCommand cmd)
    {
      string reqUrl = ComputeCommand(cmd);

      WebRequest req = HttpWebRequest.Create(reqUrl);

      req.Method = "POST";

      using (var stm = req.GetResponse().GetResponseStream())
      using (var stmReader = new StreamReader(stm))
      {
        return stmReader.ReadToEnd();
      }
    }

    /// <summary>
    /// Executes the given command and returns the typed result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public virtual T ExecuteCommand<T>(VpcCommand cmd) where T : VpcCommandResult, new()
    {
      string resultStr = ExecuteCommandRaw(cmd);
      var queryParams = QueryManager.ExtractQueryParameters(resultStr);

      T result = new T();
      result.LoadParameters(queryParams);

      return result;
    }


  }
}
