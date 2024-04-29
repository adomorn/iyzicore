using Iyzicore.Request;

namespace Iyzipay.Tests.Functional.Builder.Request;

public sealed class RetrieveCheckoutFormRequestBuilder : BaseRequestBuilder
{
    private string _token;

    private RetrieveCheckoutFormRequestBuilder()
    {
    }

    public static RetrieveCheckoutFormRequestBuilder Create()
    {
        return new RetrieveCheckoutFormRequestBuilder();
    }

    public RetrieveCheckoutFormRequestBuilder Token(string token)
    {
        _token = token;
        return this;
    }

    public RetrieveCheckoutFormRequest Build()
    {
        var retrieveCheckoutFormRequest = new RetrieveCheckoutFormRequest();
        retrieveCheckoutFormRequest.Locale = _locale;
        retrieveCheckoutFormRequest.ConversationId = _conversationId;
        retrieveCheckoutFormRequest.Token = _token;
        return retrieveCheckoutFormRequest;
    }
}