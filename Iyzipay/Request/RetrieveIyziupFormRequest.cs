using System;

namespace Iyzicore.Request
{
    public class RetrieveIyziupFormRequest : BaseRequest
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
}