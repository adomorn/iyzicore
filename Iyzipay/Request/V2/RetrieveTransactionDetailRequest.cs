namespace Iyzicore.Request.V2;

public class RetrieveTransactionDetailRequest : BaseRequestV2
{
    public string PaymentConversationId { get; set; }
    public string PaymentId { get; set; }
}