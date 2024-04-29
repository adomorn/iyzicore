﻿using System;
using Iyzicore.Request;

namespace Iyzicore.Model
{
    public class PayWithIyzico : PaymentResource
    {
        public string Token { get; set; }
        public string CallbackUrl { get; set; }      

        public static PayWithIyzico Retrieve(RetrievePayWithIyzicoRequest request, Options options)
        {
            return RestHttpClient.Create().Post<PayWithIyzico>(options.BaseUrl + "/payment/iyzipos/checkoutform/auth/ecom/detail", GetHttpHeaders(request, options), request);
        }
    }
}
