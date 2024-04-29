﻿using Iyzicore.Request;

namespace Iyzicore.Model;

public class PaymentItem : IyzipayResource
{
    public string ItemId { get; set; }
    public string PaymentTransactionId { get; set; }
    public int? TransactionStatus { get; set; }
    public string Price { get; set; }
    public string PaidPrice { get; set; }
    public string MerchantCommissionRate { get; set; }
    public string MerchantCommissionRateAmount { get; set; }
    public string IyziCommissionRateAmount { get; set; }
    public string IyziCommissionFee { get; set; }
    public string BlockageRate { get; set; }
    public string BlockageRateAmountMerchant { get; set; }
    public string BlockageRateAmountSubMerchant { get; set; }
    public string BlockageResolvedDate { get; set; }
    public string SubMerchantKey { get; set; }
    public string SubMerchantPrice { get; set; }
    public string SubMerchantPayoutRate { get; set; }
    public string SubMerchantPayoutAmount { get; set; }
    public string MerchantPayoutAmount { get; set; }
    public ConvertedPayout ConvertedPayout { get; set; }

    public static PaymentItem Update(UpdatePaymentItemRequest request, Options options)
    {
        return RestHttpClient.Create()
            .Put<PaymentItem>(options.BaseUrl + "/payment/item", GetHttpHeaders(request, options), request);
    }
}