using Iyzicore.Model;
using Iyzicore.Request;

namespace Iyzipay.Tests.Functional.Builder.Request;

public sealed class LoyaltyInquiryRequestBuilder : BaseRequestBuilder
{
    private readonly LoyaltyPaymentCard _loyaltyPaymentCard =
        LoyaltyPaymentCardBuilder.Create().BuildWithCardCredentials().Build();

    private string _currency;

    private LoyaltyInquiryRequestBuilder()
    {
    }

    public static LoyaltyInquiryRequestBuilder Create()
    {
        return new LoyaltyInquiryRequestBuilder();
    }

    public LoyaltyInquiryRequestBuilder Currency(string currency)
    {
        _currency = currency;
        return this;
    }

    public LoyaltyInquiryRequest Build()
    {
        var loyaltyInquiryRequest = new LoyaltyInquiryRequest();
        loyaltyInquiryRequest.Locale = _locale;
        loyaltyInquiryRequest.ConversationId = _conversationId;
        loyaltyInquiryRequest.PaymentCard = _loyaltyPaymentCard;
        loyaltyInquiryRequest.Currency = _currency;
        return loyaltyInquiryRequest;
    }
}