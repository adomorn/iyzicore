using System.Collections.Generic;
using Iyzicore.Model;
using Iyzicore.Request;
using Iyzipay.Tests.Functional.Util;

namespace Iyzipay.Tests.Functional.Builder.Request;

public class CreateIyziupFormInitializeRequestBuilder : BaseRequestBuilder
{
    private string _callbackUrl;
    private string _currency = Iyzicore.Model.Currency.TRY.ToString();
    private string _enabledCardFamily;
    private List<int> _enabledInstallments = new() { 2, 3, 6, 9 };
    private int _forceThreeDS;
    private InitialConsumer _initialConsumer;
    private string _merchantOrderId = RandomGenerator.RandomId;
    private List<OrderItem> _orderItems;
    private string _paidPrice;
    private string _paymentGroup = Iyzicore.Model.PaymentGroup.LISTING.ToString();
    private string _paymentSource;
    private string _preSalesContractUrl;
    private string _price;
    private string _shippingPrice;
    private string _termsUrl;

    private CreateIyziupFormInitializeRequestBuilder()
    {
    }

    public static CreateIyziupFormInitializeRequestBuilder Create()
    {
        return new CreateIyziupFormInitializeRequestBuilder();
    }

    public CreateIyziupFormInitializeRequestBuilder MerchantOrderId(string merchantOrderId)
    {
        _merchantOrderId = merchantOrderId;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder PaymentGroup(string paymentGroup)
    {
        _paymentGroup = paymentGroup;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder PaymentSource(string paymentSource)
    {
        _paymentSource = paymentSource;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder Currency(string currency)
    {
        _currency = currency;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder ForceThreeDs(int forceThreeDs)
    {
        _forceThreeDS = forceThreeDs;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder EnabledInstallments(List<int> enabledInstallments)
    {
        _enabledInstallments = enabledInstallments;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder EnabledCardFamily(string enabledCardFamily)
    {
        _enabledCardFamily = enabledCardFamily;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder Price(string price)
    {
        _price = price;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder PaidPrice(string paidPrice)
    {
        _paidPrice = paidPrice;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder ShippingPrice(string shippingPrice)
    {
        _shippingPrice = shippingPrice;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder CallbackUrl(string callbackUrl)
    {
        _callbackUrl = callbackUrl;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder TermsUrl(string termsUrl)
    {
        _termsUrl = termsUrl;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder PreSalesContractUrl(string preSalesContractUrl)
    {
        _preSalesContractUrl = preSalesContractUrl;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder OrderItems(List<OrderItem> orderItems)
    {
        _orderItems = orderItems;
        return this;
    }

    public CreateIyziupFormInitializeRequestBuilder InitialConsumer(InitialConsumer initialConsumer)
    {
        _initialConsumer = initialConsumer;
        return this;
    }

    public CreateIyziupFormInitializeRequest Build()
    {
        var createIyziupFormInitializeRequest = new CreateIyziupFormInitializeRequest();
        createIyziupFormInitializeRequest.MerchantOrderId = _merchantOrderId;
        createIyziupFormInitializeRequest.PaymentGroup = _paymentGroup;
        createIyziupFormInitializeRequest.PaymentSource = _paymentSource;
        createIyziupFormInitializeRequest.Currency = _currency;
        createIyziupFormInitializeRequest.EnabledInstallments = _enabledInstallments;
        createIyziupFormInitializeRequest.EnabledCardFamily = _enabledCardFamily;
        createIyziupFormInitializeRequest.Price = _price;
        createIyziupFormInitializeRequest.PaidPrice = _paidPrice;
        createIyziupFormInitializeRequest.ShippingPrice = _shippingPrice;
        createIyziupFormInitializeRequest.CallbackUrl = _callbackUrl;
        createIyziupFormInitializeRequest.TermsUrl = _termsUrl;
        createIyziupFormInitializeRequest.PreSalesContractUrl = _preSalesContractUrl;
        createIyziupFormInitializeRequest.ForceThreeDS = _forceThreeDS;
        createIyziupFormInitializeRequest.OrderItems = _orderItems;
        createIyziupFormInitializeRequest.InitialConsumer = _initialConsumer;
        return createIyziupFormInitializeRequest;
    }
}