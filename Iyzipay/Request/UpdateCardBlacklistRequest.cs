﻿namespace Iyzicore.Request;

public class UpdateCardBlacklistRequest : BaseRequest
{
    public string CardToken { get; set; }
    public string CardUserKey { get; set; }

    public override string ToPKIRequestString()
    {
        return ToStringRequestBuilder.NewInstance()
            .AppendSuper(base.ToPKIRequestString())
            .Append("cardToken", CardToken)
            .Append("cardUserKey", CardUserKey)
            .GetRequestString();
    }
}