﻿using Iyzicore.Request;

namespace Iyzicore.Model;

public class BasicPayment : BasicPaymentResource
{
    public static BasicPayment Create(CreateBasicPaymentRequest request, Options options)
    {
        return RestHttpClient.Create().Post<BasicPayment>(options.BaseUrl + "/payment/auth/basic",
            GetHttpHeaders(request, options), request);
    }
}