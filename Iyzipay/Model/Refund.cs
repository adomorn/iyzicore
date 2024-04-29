﻿using System;
using Iyzicore.Request;

namespace Iyzicore.Model
{
    public class Refund : IyzipayResource
    {
        public string PaymentId { get; set; }
        public string PaymentTransactionId { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string ConnectorName { get; set; }
        public string AuthCode { get; set; }
        public string HostReference { get; set; }

        public static Refund Create(CreateRefundRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Refund>(options.BaseUrl + "/payment/refund", GetHttpHeaders(request, options), request);
        }

        public static Refund CreateAmountBasedRefundRequest(CreateAmountBasedRefundRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Refund>(options.BaseUrl + "/v2/payment/refund", GetHttpHeaders(request, options), request);
        }

    }
}
