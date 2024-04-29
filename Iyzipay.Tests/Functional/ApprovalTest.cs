using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class ApprovalTest : BaseTest
{
    [Test]
    public void Should_Approve_Payment_Item()
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

        var approval = Approval.Create(approvalRequest, _options);

        Assert.AreEqual(paymentTransactionId, approval.PaymentTransactionId);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
    }
}