using Iyzicore.Model;
using Iyzicore.Model.V2.Transaction;
using Iyzicore.Request.V2;
using NUnit.Framework;

namespace Iyzipay.Samples
{
    public class RetrieveTransactionDetailSample : Sample
    {
        [Test]
        public void Should_Retrieve_Transaction()
        {
            RetrieveTransactionDetailRequest request = new RetrieveTransactionDetailRequest()
            {
                PaymentConversationId = "payment123456789x"
            };
            TransactionDetail transactionDetail = TransactionDetail.Retrieve(request, options);
            PrintResponse<TransactionDetail>(transactionDetail);
            Assert.AreEqual(Status.SUCCESS.ToString(), transactionDetail.Status);
            Assert.IsNotNull(transactionDetail.SystemTime);
            Assert.IsNull(transactionDetail.ErrorMessage);
        }
    }


}
