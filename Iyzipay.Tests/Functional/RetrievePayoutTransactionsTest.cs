using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class RetrievePayoutTransactionsTest : BaseTest
{
    [Test]
    public void Should_Retrieve_Payout_Completed_Transactions()
    {
        var request = new RetrieveTransactionsRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.Date = "2016-01-22 19:13:00";

        var payoutCompletedTransactionList = PayoutCompletedTransactionList.Retrieve(request, _options);

        PrintResponse(payoutCompletedTransactionList);

        Assert.AreEqual(Status.SUCCESS.ToString(), payoutCompletedTransactionList.Status);
        Assert.AreEqual(Locale.TR.ToString(), payoutCompletedTransactionList.Locale);
        Assert.AreEqual("123456789", payoutCompletedTransactionList.ConversationId);
        Assert.NotNull(payoutCompletedTransactionList.SystemTime);
        Assert.Null(payoutCompletedTransactionList.ErrorCode);
        Assert.Null(payoutCompletedTransactionList.ErrorGroup);
        Assert.Null(payoutCompletedTransactionList.ErrorMessage);
    }

    [Test]
    public void Should_Retrieve_Bounced_Bank_Transfers()
    {
        var request = new RetrieveTransactionsRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.Date = "2016-01-22 19:13:00";

        var bouncedBankTransferList = BouncedBankTransferList.Retrieve(request, _options);

        PrintResponse(bouncedBankTransferList);

        Assert.AreEqual(Status.SUCCESS.ToString(), bouncedBankTransferList.Status);
        Assert.AreEqual(Locale.TR.ToString(), bouncedBankTransferList.Locale);
        Assert.AreEqual("123456789", bouncedBankTransferList.ConversationId);
        Assert.NotNull(bouncedBankTransferList.SystemTime);
        Assert.Null(bouncedBankTransferList.ErrorCode);
        Assert.Null(bouncedBankTransferList.ErrorGroup);
        Assert.Null(bouncedBankTransferList.ErrorMessage);
    }
}