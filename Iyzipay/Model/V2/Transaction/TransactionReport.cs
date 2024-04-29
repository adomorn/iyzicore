using Iyzicore.Request.V2;

namespace Iyzicore.Model.V2.Transaction;

public class TransactionReport : TransactionReportResource
{
    public static TransactionReport Retrieve(RetrieveTransactionReportRequest request, Options options)
    {
        var url =
            $"{options.BaseUrl}/v2/reporting/payment/transactions?transactionDate={request.TransactionDate}&page={request.Page}";
        return RestHttpClientV2.Create()
            .Get<TransactionReport>(url, GetHttpHeadersWithUrlParams(request, url, options));
    }
}