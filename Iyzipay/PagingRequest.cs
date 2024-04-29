namespace Iyzicore;

public class PagingRequest : BaseRequestV2
{
    public int? Page { get; set; }
    public int? Count { get; set; }
}