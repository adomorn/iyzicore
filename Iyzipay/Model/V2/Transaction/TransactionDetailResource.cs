using System.Collections.Generic;

namespace Iyzicore.Model.V2.Transaction
{
    public class TransactionDetailResource : IyzipayResourceV2
    {
        public List<TransactionDetailItem> Payments { get; set; }
    }
}
