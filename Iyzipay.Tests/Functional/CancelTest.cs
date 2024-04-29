using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder.Request;
using Iyzipay.Tests.Functional.Util;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class CancelTest : BaseTest
{
    [Test]
    public void Should_Cancel_Payment()
    {
        var paymentRequest = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Build();

        var payment = Payment.Create(paymentRequest, _options);

        var cancelRequest = CreateCancelRequestBuilder.Create()
            .PaymentId(payment.PaymentId)
            .Build();

        var cancel = Cancel.Create(cancelRequest, _options);

        PrintResponse(cancel);

        Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
        Assert.AreEqual(payment.PaymentId, cancel.PaymentId);
        Assert.AreEqual("1.1", cancel.Price.RemoveTrailingZeros());
        Assert.AreEqual(Currency.TRY.ToString(), cancel.Currency);
        Assert.NotNull(cancel.SystemTime);
        Assert.Null(cancel.ErrorCode);
        Assert.Null(cancel.ErrorMessage);
        Assert.Null(cancel.ErrorGroup);
    }

    [Test]
    public void Should_Cancel_Fraudulent_Payment()
    {
        var paymentRequest = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Build();

        var payment = Payment.Create(paymentRequest, _options);

        var cancelRequest = CreateCancelRequestBuilder.Create()
            .PaymentId(payment.PaymentId)
            .Build();

        cancelRequest.Reason = RefundReason.FRAUD.ToString();
        cancelRequest.Description = "stolen card request with 11000 try payment for default sample";

        var cancel = Cancel.Create(cancelRequest, _options);

        PrintResponse(cancel);

        Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
        Assert.AreEqual(payment.PaymentId, cancel.PaymentId);
        Assert.AreEqual("1.1", cancel.Price.RemoveTrailingZeros());
        Assert.AreEqual(Currency.TRY.ToString(), cancel.Currency);
        Assert.NotNull(cancel.SystemTime);
        Assert.Null(cancel.ErrorCode);
        Assert.Null(cancel.ErrorMessage);
        Assert.Null(cancel.ErrorGroup);
    }
}