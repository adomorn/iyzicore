using Iyzicore.Request;
using Newtonsoft.Json;

namespace Iyzicore.Model;

public class BinNumber : IyzipayResource
{
    [JsonProperty(PropertyName = "binNumber")]
    public string Bin { get; set; }

    public string CardType { get; set; }
    public string CardAssociation { get; set; }
    public string CardFamily { get; set; }
    public string BankName { get; set; }
    public long BankCode { get; set; }
    public int Commercial { get; set; }

    public static BinNumber Retrieve(RetrieveBinNumberRequest request, Options options)
    {
        return RestHttpClient.Create().Post<BinNumber>($"{options.BaseUrl}/payment/bin/check",
            GetHttpHeaders(request, options), request);
    }
}