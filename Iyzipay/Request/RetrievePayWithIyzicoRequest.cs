﻿namespace Iyzicore.Request;

public class RetrievePayWithIyzicoRequest : BaseRequest
{
    public string Token { set; get; }

    public override string ToPKIRequestString()
    {
        return ToStringRequestBuilder.NewInstance()
            .AppendSuper(base.ToPKIRequestString())
            .Append("token", Token)
            .GetRequestString();
    }
}