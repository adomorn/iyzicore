using System;

namespace Iyzicore.Request
{
    public class RetrieveCardBlacklistRequest : BaseRequest
    {
        public string CardNumber { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("cardNumber", CardNumber)
                .GetRequestString();
        }
    }
}
