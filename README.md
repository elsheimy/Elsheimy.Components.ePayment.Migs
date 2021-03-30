
# Elsheimy.Components.ePayment.Migs

## Summary
This repository is a .NET wrapper around Mastercard MIGS VPC API, built using C#. It cover:

 - Server-hosted (3-party) payment command
 - Merchant-hosted (3-party) payment command
 - Query transaction command

## Usage
The following sample demonstrates how the usage of server-hosted and merchant hosted commands:

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
## Discussion
A full explanation of Mastercard VPC API is available on my blog here: [https://justlikemagic.home.blog/2021/02/02/mastercard-migs-vpc/](https://justlikemagic.home.blog/2021/02/02/mastercard-migs-vpc/)
## NuGet
This components is also available on Nuget at [https://www.nuget.org/packages/Elsheimy.Components.ePayment.Migs/](https://www.nuget.org/packages/Elsheimy.Components.ePayment.Migs/)
