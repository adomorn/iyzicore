using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class DisapproveTest : BaseTest
{
    [Test]
    public void Should_Disapprove_Payment()
    {
        var request = CreateSubMerchantRequestBuilder.Create()
            .PersonalSubMerchantRequest()
            .Build();

        var subMerchant = SubMerchant.Create(request, _options);

        var paymentRequest = CreatePaymentRequestBuilder.Create()
            .MarketplacePayment(subMerchant.SubMerchantKey)
            .Build();

        var payment = Payment.Create(paymentRequest, _options);

        var paymentTransactionId = payment.PaymentItems[0].PaymentTransactionId;

        var approvalRequest = CreateApprovalRequestBuilder.Create()
            .PaymentTransactionId(paymentTransactionId)
            .Build();

        Approval.Create(approvalRequest, _options);

        var disapproval = Disapproval.Create(approvalRequest, _options);

        PrintResponse(disapproval);

        Assert.AreEqual(paymentTransactionId, disapproval.PaymentTransactionId);
        Assert.AreEqual(Status.SUCCESS.ToString(), disapproval.Status);
        Assert.AreEqual(Locale.TR.ToString(), disapproval.Locale);
        Assert.NotNull(disapproval.SystemTime);
        Assert.Null(disapproval.ErrorCode);
        Assert.Null(disapproval.ErrorMessage);
        Assert.Null(disapproval.ErrorGroup);
    }
}