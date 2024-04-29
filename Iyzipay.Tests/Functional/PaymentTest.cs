using Iyzicore;
using Iyzicore.Model;
using Iyzicore.Request;
using Iyzipay.Tests.Functional.Builder;
using Iyzipay.Tests.Functional.Builder.Request;
using Iyzipay.Tests.Functional.Util;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class PaymentTest : BaseTest
{
    [Test]
    public void Should_Create_Listing_Payment()
    {
        var request = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Build();

        var payment = Payment.Create(request, _options);

        PrintResponse(payment);

        Assert.Null(payment.ConnectorName);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.PaymentId);
        Assert.NotNull(payment.BasketId);
        Assert.AreEqual(payment.Price, "1");
        Assert.AreEqual(payment.PaidPrice, "1.1");
        Assert.AreEqual(payment.IyziCommissionRateAmount.RemoveTrailingZeros(), "0.028875");
        Assert.AreEqual(payment.IyziCommissionFee.RemoveTrailingZeros(), "0.25");
        Assert.AreEqual(payment.MerchantCommissionRate.RemoveTrailingZeros(), "10");
        Assert.AreEqual(payment.MerchantCommissionRateAmount.RemoveTrailingZeros(), "0.1");
    }

    [Test]
    public void Should_Create_Marketplace_Payment()
    {
        var createSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .PersonalSubMerchantRequest()
            .Build();

        var subMerchantKey = SubMerchant.Create(createSubMerchantRequest, _options).SubMerchantKey;
        var request = CreatePaymentRequestBuilder.Create()
            .MarketplacePayment(subMerchantKey)
            .Build();

        var payment = Payment.Create(request, _options);

        PrintResponse(payment);

        Assert.Null(payment.ConnectorName);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.PaymentId);
        Assert.NotNull(payment.BasketId);
        Assert.AreEqual("1", payment.Price);
        Assert.AreEqual("1.1", payment.PaidPrice);
        Assert.AreEqual("0.028875", payment.IyziCommissionRateAmount.RemoveTrailingZeros());
        Assert.AreEqual("0.25", payment.IyziCommissionFee.RemoveTrailingZeros());
        Assert.AreEqual("10", payment.MerchantCommissionRate.RemoveTrailingZeros());
        Assert.AreEqual("0.1", payment.MerchantCommissionRateAmount.RemoveTrailingZeros());
        Assert.AreEqual(1, payment.Installment);
    }

    [Test]
    public void Should_Create_Payment_With_Registered_Card()
    {
        var externalUserId = RandomGenerator.RandomId;
        var cardInformation = CardInformationBuilder.Create()
            .Build();

        var cardRequest = CreateCardRequestBuilder.Create()
            .Card(cardInformation)
            .ExternalId(externalUserId)
            .Email("email@email.com")
            .Build();

        var card = Card.Create(cardRequest, _options);

        var paymentCard = PaymentCardBuilder.Create()
            .CardUserKey(card.CardUserKey)
            .CardToken(card.CardToken)
            .Build();

        var request = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .PaymentCard(paymentCard)
            .Build();

        var payment = Payment.Create(request, _options);

        PrintResponse(payment);

        Assert.Null(payment.ConnectorName);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.AreEqual("123456789", payment.ConversationId);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.PaymentId);
        Assert.NotNull(payment.BasketId);
        Assert.AreEqual("1", payment.Price);
        Assert.AreEqual("1.1", payment.PaidPrice.RemoveTrailingZeros());
        Assert.AreEqual("0.028875", payment.IyziCommissionRateAmount.RemoveTrailingZeros());
        Assert.AreEqual("0.25", payment.IyziCommissionFee.RemoveTrailingZeros());
        Assert.AreEqual("10", payment.MerchantCommissionRate.RemoveTrailingZeros());
        Assert.AreEqual("0.1", payment.MerchantCommissionRateAmount.RemoveTrailingZeros());
        Assert.AreEqual(1, payment.Installment);
    }

    [Test]
    public void Should_Retrieve_Payment()
    {
        var request = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Build();

        var createdPayment = Payment.Create(request, _options);

        PrintResponse(createdPayment);

        var retrievePaymentRequest = new RetrievePaymentRequest();
        retrievePaymentRequest.Locale = Locale.TR.ToString();
        retrievePaymentRequest.ConversationId = "123456789";
        retrievePaymentRequest.PaymentId = createdPayment.PaymentId;

        var payment = Payment.Retrieve(retrievePaymentRequest, _options);

        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.AreEqual(1, payment.Installment);
        Assert.AreEqual("123456789", payment.ConversationId);
        Assert.AreEqual(createdPayment.PaymentId, payment.PaymentId);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.BasketId);
    }

    [Test]
    public void Should_Create_Payment_With_Loyalty_Ykb_World()
    {
        var reward = new LoyaltyReward();
        reward.RewardUsage = 1;
        reward.RewardAmount = "0.1";

        var request = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Reward(reward)
            .PaymentCard(PaymentCardBuilder.Create().BuildWithYKBCardCredentials().Build())
            .Build();

        var payment = Payment.Create(request, _options);

        PrintResponse(payment);

        Assert.Null(payment.ConnectorName);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.PaymentId);
        Assert.NotNull(payment.BasketId);
        Assert.AreEqual(payment.Price, "1");
        Assert.AreEqual(payment.PaidPrice, "1.1");
        Assert.AreEqual(payment.IyziCommissionRateAmount.RemoveTrailingZeros(), "0.028875");
        Assert.AreEqual(payment.IyziCommissionFee.RemoveTrailingZeros(), "0.25");
        Assert.AreEqual(payment.MerchantCommissionRate.RemoveTrailingZeros(), "10");
        Assert.AreEqual(payment.MerchantCommissionRateAmount.RemoveTrailingZeros(), "0.1");
    }

    [Test]
    public void Should_Create_Payment_With_Loyalty_Denizbank_Bonus()
    {
        var reward = new LoyaltyReward();
        reward.RewardUsage = 1;
        reward.RewardAmount = "0.1";

        var request = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .Reward(reward)
            .PaymentCard(PaymentCardBuilder.Create().BuildWithDenizBankCardCredentials().Build())
            .Build();

        var payment = Payment.Create(request, _options);

        PrintResponse(payment);

        Assert.Null(payment.ConnectorName);
        Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
        Assert.NotNull(payment.SystemTime);
        Assert.Null(payment.ErrorCode);
        Assert.Null(payment.ErrorMessage);
        Assert.Null(payment.ErrorGroup);
        Assert.NotNull(payment.PaymentId);
        Assert.NotNull(payment.BasketId);
        Assert.AreEqual(payment.Price, "1");
        Assert.AreEqual(payment.PaidPrice, "1.1");
        Assert.AreEqual(payment.IyziCommissionRateAmount.RemoveTrailingZeros(), "0.028875");
        Assert.AreEqual(payment.IyziCommissionFee.RemoveTrailingZeros(), "0.25");
        Assert.AreEqual(payment.MerchantCommissionRate.RemoveTrailingZeros(), "10");
        Assert.AreEqual(payment.MerchantCommissionRateAmount.RemoveTrailingZeros(), "0.1");
    }
}