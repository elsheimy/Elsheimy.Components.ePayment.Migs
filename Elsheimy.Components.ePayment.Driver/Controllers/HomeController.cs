using Elsheimy.Components.ePayment.Driver.Models;
using Elsheimy.Components.ePayment.Migs;
using Elsheimy.Components.ePayment.Migs.Commands;
using Elsheimy.Components.ePayment.Migs.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Elsheimy.Components.ePayment.Driver.Controllers
{
  public class HomeController : Controller
  {
    private static string _defaultHashSecret = "7E5C2F4D270600C61F5386167ECB8DA6";
    public IActionResult Index(IndexViewModel model)
    {

      // Initialize default values
      model.MerchantID = "TESTEGPTEST";
      model.AccessCode = "77426638";
      model.HashSecret = _defaultHashSecret;
      model.Amount = 100.25M;
      model.Locale = "en";
      model.TransactionReference = "TX-1";
      model.OrderInfo = "100";

      model.CardNumber = 5123456789012346;
      model.CardExpiryMonth = 05;
      model.CardExpiryYear = 2021;
      model.CardSecurityCode = 100;

      return View(model);
    }

    public IActionResult Results()
    {
      return View();
    }

    [HttpPost]
    public IActionResult ExecuteServerHostedCommand(IndexViewModel model)
    {
      VpcClient client = new VpcClient(model.MerchantID, model.AccessCode, model.HashSecret);

      VpcServerHostedPaymentCommand cmd = new VpcServerHostedPaymentCommand();
      cmd.ActualAmount = model.Amount;
      cmd.MerchantTxnReference = model.TransactionReference;
      cmd.OrderInfo = model.OrderInfo;
      cmd.ReturnUrl = this.GetAbsoluteUrl() + UrlHelperExtensions.Action(this.Url, nameof(ServerHostedPaymentCallback));
      cmd.Locale = model.Locale;

      string url = client.ComputeCommand(cmd);

      return Redirect(url);
    }


    public IActionResult ServerHostedPaymentCallback()
    {
      var queryParameters = HttpContext.Request.Query.Select(a => new QueryParameter(a.Key, a.Value));
      List<QueryParameter> paramList = new List<QueryParameter>();

      foreach (var par in queryParameters)
      {
        paramList.Add(new QueryParameter(par.Name, par.Value));
      }

      VpcPaymentResult result = new VpcPaymentResult();
      result.LoadParameters(paramList);


      TempData["QueryParams"] = JsonConvert.SerializeObject(result, Formatting.Indented);
      return RedirectToAction(nameof(Results));
    }


    [HttpPost]
    public IActionResult ExecuteMerchantHostedCommand(IndexViewModel model)
    {
      VpcClient client = new VpcClient(model.MerchantID, model.AccessCode, model.HashSecret);

      VpcMerchantHostedPaymentCommand cmd = new VpcMerchantHostedPaymentCommand();
      cmd.ActualAmount = model.Amount;
      cmd.MerchantTxnReference = model.TransactionReference;
      cmd.OrderInfo = model.OrderInfo;
      cmd.CardNumber = model.CardNumber;
      cmd.CardExpiryYear = model.CardExpiryYear;
      cmd.CardExpiryMonth = model.CardExpiryMonth;
      cmd.CardSecurityCode = model.CardSecurityCode;

      VpcPaymentResult result = client.ExecuteCommand<VpcPaymentResult>(cmd);

      TempData["QueryParams"] = JsonConvert.SerializeObject(result, Formatting.Indented);
      return RedirectToAction(nameof(Results));
    }

    private string GetAbsoluteUrl()
    {
      HttpRequest request = HttpContext.Request;
      return request.Scheme + "://" + request.Host.Value;
    }

  }
}
