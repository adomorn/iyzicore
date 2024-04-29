using Iyzicore.Request;

namespace Iyzicore.Model;

public class UcsInit : IyzipayResourceV2
{
    public string UcsToken { get; set; }
    public string BuyerProtectedConsumer { get; set; }
    public string BuyerProtectedMerchant { get; set; }
    public string GsmNumber { get; set; }
    public string MaskedGsmNumber { get; set; }
    public string MerchantName { get; set; }
    public string Script { get; set; }
    public string ScriptType { get; set; }

    public static UcsInit Create(InitUcsRequest request, Options options)
    {
        var uri = $"{options.BaseUrl}/v2/ucs/init";
        var response = RestHttpClientV2.Create()
            .Post<UcsInit>(uri, GetHttpHeadersWithRequestBody(request, uri, options), request);
        return response;
    }
}