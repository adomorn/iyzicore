﻿using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class RefundSample : Sample
{
    [Test]
    public void Should_Refund()
    {
        var request = new CreateRefundRequest();
        request.ConversationId = "123456789";
        request.Locale = Locale.TR.ToString();
        request.PaymentTransactionId = "1";
        request.Price = "0.5";
        request.Ip = "85.34.78.112";
        request.Currency = Currency.TRY.ToString();

        var refund = Refund.Create(request, options);

        PrintResponse(refund);

        Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
        Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
        Assert.AreEqual("123456789", refund.ConversationId);
        Assert.IsNotNull(refund.SystemTime);
        Assert.IsNull(refund.ErrorCode);
        Assert.IsNull(refund.ErrorMessage);
        Assert.IsNull(refund.ErrorGroup);
    }

    [Test]
    public void Should_Amount_Based_Refund()
    {
        var request = new CreateAmountBasedRefundRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "--";
        request.Ip = "85.34.78.112";
        request.Price = "2";
        request.PaymentId = "12425590";

        var amountBasedRefund = Refund.CreateAmountBasedRefundRequest(request, options);

        PrintResponse(amountBasedRefund);

        Assert.AreEqual(Status.SUCCESS.ToString(), amountBasedRefund.Status);
        Assert.AreEqual("10", amountBasedRefund.Price);
        Assert.AreEqual(Locale.TR.ToString(), amountBasedRefund.Locale);
        Assert.AreEqual("--", amountBasedRefund.ConversationId);
        Assert.IsNotNull(amountBasedRefund.SystemTime);
        Assert.IsNull(amountBasedRefund.ErrorCode);
        Assert.IsNull(amountBasedRefund.ErrorMessage);
        Assert.IsNull(amountBasedRefund.ErrorGroup);
    }


    public void Should_Refund_With_Reason_And_Description()
    {
        var request = new CreateRefundRequest();
        request.ConversationId = "123456789";
        request.Locale = Locale.TR.ToString();
        request.PaymentTransactionId = "1";
        request.Price = "0.5";
        request.Ip = "85.34.78.112";
        request.Currency = Currency.TRY.ToString();
        request.Reason = RefundReason.OTHER.ToString();
        request.Description = "customer requested for default sample";

        var refund = Refund.Create(request, options);

        PrintResponse(refund);

        Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
        Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
        Assert.AreEqual("123456789", refund.ConversationId);
        Assert.IsNotNull(refund.SystemTime);
        Assert.IsNull(refund.ErrorCode);
        Assert.IsNull(refund.ErrorMessage);
        Assert.IsNull(refund.ErrorGroup);
    }
}