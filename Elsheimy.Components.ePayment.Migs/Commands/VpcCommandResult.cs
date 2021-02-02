using Elsheimy.Components.ePayment.Migs.Security;
using Elsheimy.Components.ePayment.Migs.Web;
using System.Collections.Generic;
using System.Linq;

namespace Elsheimy.Components.ePayment.Migs.Commands
{
  public abstract class VpcCommandResult
  {
    /// <summary>
    /// Command name.
    /// </summary>
    [QueryParam(Name = "vpc_Command")]
    public string Command { get; set; }

    /// <summary>
    /// API version.
    /// </summary>
    [QueryParam(Name = "vpc_Version")]
    public int Version { get; set; }

    /// <summary>
    /// The two-letters or four-letters locale name.
    /// </summary>
    [QueryParam(Name = "vpc_Locale")]
    public string Locale { get; set; }

    /// <summary>
    /// The secure hash used for integrity checking.
    /// </summary>
    [QueryParam(Name = "vpc_SecureHash")]
    public string SecureHash { get; set; }
    public string SecureHashParamName { get { return "vpc_SecureHash"; } }
    /// <summary>
    /// Secure hash algorithm.
    /// </summary>
    [QueryParam(Name = "vpc_SecureHashType")]
    public string SecureHashType { get; set; }
    public string SecureHashTypeParamName { get { return "vpc_SecureHashType"; } }
    /// <summary>
    /// Merchant ID.
    /// </summary>
    [QueryParam(Name = "vpc_Merchant")]
    public string MerchantID { get; set; }
    public SecureHashService SecureHashService { get; set; }


    public VpcCommandResult()
    {
      SecureHashService = new SecureHashService();
    }


    /// <summary>
    /// Loads the given query parameters.
    /// </summary>
    /// <param name="parameters"></param>
    public virtual void LoadParameters(IEnumerable<QueryParameter> parameters)
    {
      QueryManager.ApplyQueryParameters(this, parameters);
    }

    /// <summary>
    /// Returns query parameters in the current object.
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<QueryParameter> GetQueryParams()
    {
      return QueryManager.GenerateQueryParameters(this);
    }

    /// <summary>
    /// Validates the current results using the given hash secret and hash type.
    /// </summary>
    /// <param name="hashSecret"></param>
    /// <returns></returns>
    public bool ValidateResult(string hashSecret, VpcSecureHashType hashType)
    {
      var queryParams = QueryManager.GenerateQueryParameters(this);
      // remove secure hash params
      queryParams = queryParams
        .Except(queryParams.Where(a => a.Name == SecureHashParamName || a.Name == SecureHashTypeParamName));

      string generatedSecureHash =
          SecureHashService.GenerateSecureHash(hashSecret, queryParams, hashType);

      return string.Compare(generatedSecureHash, this.SecureHash, false) == 0;
    }
  }
}
