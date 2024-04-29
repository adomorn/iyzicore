using Iyzicore.Request;

namespace Iyzipay.Tests.Functional.Builder.Request;

public sealed class RetrieveBinNumberRequestBuilder : BaseRequestBuilder
{
    private string _binNumber;

    private RetrieveBinNumberRequestBuilder()
    {
    }

    public static RetrieveBinNumberRequestBuilder Create()
    {
        return new RetrieveBinNumberRequestBuilder();
    }

    public RetrieveBinNumberRequestBuilder BinNumber(string binNumber)
    {
        _binNumber = binNumber;
        return this;
    }

    public RetrieveBinNumberRequest Build()
    {
        var retrieveBinNumberRequest = new RetrieveBinNumberRequest();
        retrieveBinNumberRequest.Locale = _locale;
        retrieveBinNumberRequest.ConversationId = _conversationId;
        retrieveBinNumberRequest.BinNumber = _binNumber;
        return retrieveBinNumberRequest;
    }
}