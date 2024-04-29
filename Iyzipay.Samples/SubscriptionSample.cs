using System;
using System.Linq;
using Iyzicore.Model;
using Iyzicore.Model.V2.Subscription;
using Iyzicore.Request.V2.Subscription;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class SubscriptionSample : Sample
{
    [Test]
    public void Should_Initialize_CheckoutForm()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var request = new InitializeCheckoutFormRequest
        {
            Locale = Locale.TR.ToString(),
            Customer = new CheckoutFormCustomer
            {
                Email = $"iyzico-{randomString}@iyzico.com",
                Name = "customer-name",
                Surname = "customer-surname",
                BillingAddress = new Address
                {
                    City = "İstanbul",
                    Country = "Türkiye",
                    Description = "billing-address-description",
                    ContactName = "billing-contact-name",
                    ZipCode = "010101"
                },
                ShippingAddress = new Address
                {
                    City = "İstanbul",
                    Country = "Türkiye",
                    Description = "shipping-address-description",
                    ContactName = "shipping-contact-name",
                    ZipCode = "010102"
                },
                GsmNumber = "+905350000000",
                IdentityNumber = "55555555555"
            },
            CallbackUrl = "https://www.google.com",
            ConversationId = "123456789",
            PricingPlanReferenceCode = "pricingPlanReferenceCode",
            SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
        };

        var response = Subscription.InitializeCheckoutForm(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.IsNotNull(response.CheckoutFormContent);
        Assert.IsNotNull(response.Token);
        Assert.IsNotNull(response.TokenExpireTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Initialize_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var request = new SubscriptionInitializeRequest
        {
            Locale = Locale.TR.ToString(),
            Customer = new CheckoutFormCustomer
            {
                Email = $"iyzico-{randomString}@iyzico.com",
                Name = "customer-name",
                Surname = "customer-surname",
                BillingAddress = new Address
                {
                    City = "İstanbul",
                    Country = "Türkiye",
                    Description = "billing-address-description",
                    ContactName = "billing-contact-name",
                    ZipCode = "010101"
                },
                ShippingAddress = new Address
                {
                    City = "İstanbul",
                    Country = "Türkiye",
                    Description = "shipping-address-description",
                    ContactName = "shipping-contact-name",
                    ZipCode = "010102"
                },

                GsmNumber = "+905350000000",
                IdentityNumber = "55555555555"
            },
            PaymentCard = new CardInfo
            {
                CardNumber = "5528790000000008",
                CardHolderName = "iyzico",
                ExpireMonth = "12",
                ExpireYear = "2029",
                Cvc = "123",
                RegisterConsumerCard = true
            },
            ConversationId = "123456789",
            PricingPlanReferenceCode = "pricingPlanReferenceCode"
        };

        var response = Subscription.Initialize(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.Data.ReferenceCode);
        Assert.NotNull(response.Data.ParentReferenceCode);
        Assert.AreEqual("pricingPlanReferenceCode", response.Data.PricingPlanReferenceCode);
        Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
        Assert.AreEqual(3, response.Data.TrialDays);
        Assert.NotNull(response.Data.TrialStartDate);
        Assert.NotNull(response.Data.TrialEndDate);
        Assert.NotNull(response.Data.StartDate);
    }

    [Test]
    public void Should_Activate_Subscription()
    {
        var request = new ActivateSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = "subscriptionReferenceCode"
        };

        var response = Subscription.Activate(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Retry_Subscription()
    {
        var request = new RetrySubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionOrderReferenceCode = "referenceCode"
        };

        var response = Subscription.Retry(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Upgrade_Subscription()
    {
        var request = new UpgradeSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = "subscriptionReferenceCode",
            NewPricingPlanReferenceCode = "newPricingPlanReferenceCode",
            UseTrial = true,
            ResetRecurrenceCount = true,
            UpgradePeriod = SubscriptionUpgradePeriod.NOW.ToString()
        };

        var response = Subscription.Upgrade(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Cancel_Subscription()
    {
        var request = new CancelSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = "subscriptionReferenceCode"
        };

        var response = Subscription.Cancel(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Retrieve_Subscription()
    {
        var request = new RetrieveSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = "subscriptionReferenceCode"
        };

        var response = Subscription.Retrieve(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.AreEqual("subscriptionReferenceCode", response.Data.ReferenceCode);
    }

    [Test]
    public void Should_Search_Subscription()
    {
        var request = new SearchSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = "subscriptionReferenceCode",
            Page = 1,
            Count = 1,
            SubscriptionStatus = SubscriptionStatus.ACTIVE.ToString(),
            PricingPlanReferenceCode = "pricingPlanReferenceCode"
        };

        var response = Subscription.Search(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.AreEqual(1, response.Data.Items.Count);
        Assert.AreEqual(1, response.Data.CurrentPage);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.Data.Items.First().ReferenceCode);
        Assert.NotNull(response.Data.Items.First().ParentReferenceCode);
        Assert.AreEqual("pricingPlanReferenceCode", response.Data.Items.First().PricingPlanReferenceCode);
        Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.Items.First().SubscriptionStatus);
    }

    [Test]
    public void Should_Update_Subscription_Card()
    {
        var request = new UpdateCardRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            CustomerReferenceCode = "customerReferenceCode",
            CallbackUrl = "https://www.google.com"
        };

        var response = Subscription.UpdateCard(request, options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.CheckoutFormContent);
        Assert.NotNull(response.Token);
        Assert.NotNull(response.TokenExpireTime);
    }
}