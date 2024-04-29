using System.Collections.Generic;
using System.Linq;
using Iyzicore.Model;
using Iyzicore.Request;
using Iyzipay.Tests.Functional.Util;

namespace Iyzipay.Tests.Functional.Builder.Request;

public sealed class CreateCheckoutFormInitializeRequestBuilder : BaseRequestBuilder
{
    private string _basketId = RandomGenerator.RandomId;
    private IEnumerable<BasketItem> _basketItems;
    private Address _billingAddress = AddressBuilder.Create().Build();
    private Buyer _buyer = BuyerBuilder.Create().Build();
    private string _callbackUrl;
    private string _cardUserKey;
    private string _currency = Iyzicore.Model.Currency.TRY.ToString();
    private List<int> _enabledInstallments = new() { 2, 3, 6, 9 };
    private int _forceThreeDs;
    private string _paidPrice;
    private string _paymentGroup = Iyzicore.Model.PaymentGroup.LISTING.ToString();
    private string _paymentSource;
    private string _posOrderId;
    private string _price;
    private Address _shippingAddress = AddressBuilder.Create().Build();

    private CreateCheckoutFormInitializeRequestBuilder()
    {
    }

    public static CreateCheckoutFormInitializeRequestBuilder Create()
    {
        return new CreateCheckoutFormInitializeRequestBuilder();
    }

    public CreateCheckoutFormInitializeRequestBuilder Price(string price)
    {
        _price = price;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder PaidPrice(string paidPrice)
    {
        _paidPrice = paidPrice;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder BasketId(string basketId)
    {
        _basketId = basketId;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder PaymentGroup(string paymentGroup)
    {
        _paymentGroup = paymentGroup;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder PaymentSource(string paymentSource)
    {
        _paymentSource = paymentSource;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder Currency(string currency)
    {
        _currency = currency;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder Buyer(Buyer buyer)
    {
        _buyer = buyer;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder ShippingAddress(Address shippingAddress)
    {
        _shippingAddress = shippingAddress;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder BillingAddress(Address billingAddress)
    {
        _billingAddress = billingAddress;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder BasketItems(IEnumerable<BasketItem> basketItems)
    {
        _basketItems = basketItems;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder CallbackUrl(string callbackUrl)
    {
        _callbackUrl = callbackUrl;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder ForceThreeDs(int forceThreeDs)
    {
        _forceThreeDs = forceThreeDs;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder CardUserKey(string cardUserKey)
    {
        _cardUserKey = cardUserKey;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder PosOrderId(string posOrderId)
    {
        _posOrderId = posOrderId;
        return this;
    }

    public CreateCheckoutFormInitializeRequestBuilder EnabledInstallments(List<int> enabledInstallments)
    {
        _enabledInstallments = enabledInstallments;
        return this;
    }

    public CreateCheckoutFormInitializeRequest Build()
    {
        var createCheckoutFormInitializeRequest = new CreateCheckoutFormInitializeRequest();
        createCheckoutFormInitializeRequest.Locale = _locale;
        createCheckoutFormInitializeRequest.ConversationId = _conversationId;
        createCheckoutFormInitializeRequest.Price = _price;
        createCheckoutFormInitializeRequest.PaidPrice = _paidPrice;
        createCheckoutFormInitializeRequest.BasketId = _basketId;
        createCheckoutFormInitializeRequest.PaymentGroup = _paymentGroup;
        createCheckoutFormInitializeRequest.PaymentSource = _paymentSource;
        createCheckoutFormInitializeRequest.Currency = _currency;
        createCheckoutFormInitializeRequest.Buyer = _buyer;
        createCheckoutFormInitializeRequest.ShippingAddress = _shippingAddress;
        createCheckoutFormInitializeRequest.BillingAddress = _billingAddress;
        createCheckoutFormInitializeRequest.BasketItems = _basketItems.ToList();
        createCheckoutFormInitializeRequest.CallbackUrl = _callbackUrl;
        createCheckoutFormInitializeRequest.ForceThreeDS = _forceThreeDs;
        createCheckoutFormInitializeRequest.CardUserKey = _cardUserKey;
        createCheckoutFormInitializeRequest.PosOrderId = _posOrderId;
        createCheckoutFormInitializeRequest.EnabledInstallments = _enabledInstallments;
        return createCheckoutFormInitializeRequest;
    }
}