﻿using Iyzicore.Model;
using Iyzicore.Request;
using Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class ThreedsTest : BaseTest
{
    [Test]
    public void Should_Create_Payment_With_Physical_And_Virtual_Item_For_Standard_Merchant()
    {
        var createPaymentRequest = CreatePaymentRequestBuilder.Create()
            .StandardListingPayment()
            .CallbackUrl("https://www.merchant.com/callback")
            .Build();

        var threedsInitialize = ThreedsInitialize.Create(createPaymentRequest, _options);

        PrintResponse(threedsInitialize);

        Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
        Assert.NotNull(threedsInitialize.SystemTime);
        Assert.NotNull(threedsInitialize.HtmlContent);
        Assert.Null(threedsInitialize.ErrorCode);
        Assert.Null(threedsInitialize.ErrorMessage);
        Assert.Null(threedsInitialize.ErrorGroup);
    }

    [Test]
    public void Should_Create_Threeds_Payment_With_Physical_And_Virtual_Item_For_Marketplace_Merchant()
    {
        var createSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .PersonalSubMerchantRequest()
            .Build();

        var subMerchant = SubMerchant.Create(createSubMerchantRequest, _options);

        var createPaymentRequest = CreatePaymentRequestBuilder.Create()
            .MarketplacePayment(subMerchant.SubMerchantKey)
            .CallbackUrl("https://www.merchant.com/callback")
            .Build();

        var threedsInitialize = ThreedsInitialize.Create(createPaymentRequest, _options);

        PrintResponse(threedsInitialize);

        Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
        Assert.NotNull(threedsInitialize.SystemTime);
        Assert.NotNull(threedsInitialize.HtmlContent);
        Assert.Null(threedsInitialize.ErrorCode);
        Assert.Null(threedsInitialize.ErrorMessage);
        Assert.Null(threedsInitialize.ErrorGroup);
    }

    /*
        This test needs manual payment from Pecco on sandbox environment. So it does not contain any assertions.
    */
    [Test]
    public void Should_Auth_Threeds()
    {
        var createThreedsPaymentRequest = new CreateThreedsPaymentRequest();
        createThreedsPaymentRequest.ConversationData = "conversion data";
        createThreedsPaymentRequest.PaymentId = "1";
        createThreedsPaymentRequest.Locale = Locale.TR.ToString();
        createThreedsPaymentRequest.ConversationId = "123456789";

        var threedsPayment = ThreedsPayment.Create(createThreedsPaymentRequest, _options);

        PrintResponse(threedsPayment);
    }
}