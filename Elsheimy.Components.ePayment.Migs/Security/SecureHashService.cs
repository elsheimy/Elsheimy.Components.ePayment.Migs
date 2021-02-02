using Elsheimy.Components.ePayment.Migs.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Elsheimy.Components.ePayment.Migs.Security
{
  public class SecureHashService
  {
    /// <summary>
    /// Generates a secure hash using the required algorithm and the provided hash secret.
    /// </summary>
    /// <returns></returns>
    public virtual string GenerateSecureHash(string hashSecret, IEnumerable<QueryParameter> queryParams, VpcSecureHashType secureHashType)
    {
      if (secureHashType == VpcSecureHashType.SHA256)
        return SHA256Hash(hashSecret, queryParams);
      else if (secureHashType == VpcSecureHashType.MD5)
        return MD5Hash(hashSecret, queryParams);
      else
        return null;
    }


    public virtual string SHA256Hash(string hashSecret, IEnumerable<QueryParameter> queryParams)
    {
      queryParams = queryParams.OrderBy(a => a.Name, StringComparer.Ordinal);
      string queryStr = QueryManager.CreateQueryString(queryParams);
      return Sha256(queryStr, hashSecret);
    }


    public virtual string MD5Hash(string hashSecret, IEnumerable<QueryParameter> queryParams)
    {
      queryParams = queryParams.OrderBy(a => a.Name, StringComparer.Ordinal);

      string str = hashSecret + string.Join("", queryParams.Select(a => a.Value));

      return MD5(str);
    }


    public virtual string MD5(string input)
    {
      return Hash(input, new MD5CryptoServiceProvider());
    }
    public virtual string Sha256(string input, string key)
    {
      //Hex Decode the Secure Secret for use in using the HMACSHA256 hasher
      //hex decoding eliminates this source of error as it is independent of the character encoding
      //hex decoding is precise in converting to a byte array and is the preferred form for representing binary values as hex strings.


      byte[] convertedHash = new byte[key.Length / 2];
      for (int i = 0; i < key.Length / 2; i++)
        convertedHash[i] = (byte)Int32.Parse(key.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

      HMACSHA256 algorithm = new HMACSHA256(convertedHash);

      return Hash(input, algorithm);
    }


    public virtual string Hash(string input, HashAlgorithm algorithm)
    {
      byte[] data = null;

      if (algorithm != null)
        data = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
      else
        data = System.Text.Encoding.UTF8.GetBytes(input);

      string hash = string.Empty;
      foreach (var b in data)
        hash += b.ToString("X2");

      return hash.ToUpper();
    }

  }
}
