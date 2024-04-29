using System;
using System.Linq;
using Iyzicore.Model;
using Iyzicore.Model.V2.Subscription;
using Iyzicore.Request.V2.Subscription;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class SubscriptionTest : BaseTest
{
    [Test]
    public void Should_Initialize_CheckoutForm()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
        };

        var response = Subscription.InitializeCheckoutForm(request, _options);
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
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

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
            PricingPlanReferenceCode = planResource.ReferenceCode
        };

        var response = Subscription.Initialize(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.Data.ReferenceCode);
        Assert.NotNull(response.Data.ParentReferenceCode);
        Assert.AreEqual(planResource.ReferenceCode, response.Data.PricingPlanReferenceCode);
        Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
        Assert.AreEqual(3, response.Data.TrialDays);
        Assert.NotNull(response.Data.TrialStartDate);
        Assert.NotNull(response.Data.TrialEndDate);
        Assert.NotNull(response.Data.StartDate);
    }

    [Test]
    public void Should_Activate_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var request = new ActivateSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
        };

        var response = Subscription.Activate(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Ignore(
        "Test needs failed payment (OrderStatus=Failed,SubscriptionStatus=Unpaid), but we can not supply this condition in test now.")]
    public void Should_Retry_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
                Cvc = "123"
            },
            ConversationId = "123456789",
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var retrieveSubscriptionRequest = new RetrieveSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
        };
        var subscriptionResponse = Subscription.Retrieve(retrieveSubscriptionRequest, _options);

        var request = new RetrySubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionOrderReferenceCode =
                subscriptionResponse.Data.SubscriptionOrders.FirstOrDefault()?.ReferenceCode
        };

        var response = Subscription.Retry(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Upgrade_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var newPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"new-plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 2,
            Price = "3.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 2,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var newPlanResource = Plan.Create(newPlanRequest, _options).Data;

        var request = new UpgradeSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode,
            NewPricingPlanReferenceCode = newPlanResource.ReferenceCode,
            UseTrial = true,
            ResetRecurrenceCount = true,
            UpgradePeriod = SubscriptionUpgradePeriod.NOW.ToString()
        };

        var response = Subscription.Upgrade(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Cancel_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var request = new CancelSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
        };

        var response = Subscription.Cancel(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Retrieve_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var request = new RetrieveSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
        };

        var response = Subscription.Retrieve(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.Data.ReferenceCode);
        Assert.NotNull(response.Data.ParentReferenceCode);
        Assert.AreEqual(planResource.ReferenceCode, response.Data.PricingPlanReferenceCode);
        Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
        Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.CustomerEmail);
        Assert.AreEqual(3, response.Data.TrialDays);
        Assert.NotNull(response.Data.TrialStartDate);
        Assert.NotNull(response.Data.TrialEndDate);
        Assert.NotNull(response.Data.StartDate);
    }

    [Test]
    public void Should_Search_Subscription()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var request = new SearchSubscriptionRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode,
            Page = 1,
            Count = 1,
            SubscriptionStatus = SubscriptionStatus.ACTIVE.ToString(),
            PricingPlanReferenceCode = planResource.ReferenceCode
        };

        var response = Subscription.Search(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.AreEqual(1, response.Data.Items.Count);
        Assert.AreEqual(1, response.Data.CurrentPage);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.Data.Items.First().ReferenceCode);
        Assert.NotNull(response.Data.Items.First().ParentReferenceCode);
        Assert.AreEqual(planResource.ReferenceCode, response.Data.Items.First().PricingPlanReferenceCode);
        Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.Items.First().SubscriptionStatus);
        Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.Items.First().CustomerEmail);
        Assert.AreEqual(3, response.Data.Items.First().TrialDays);
        Assert.NotNull(response.Data.Items.First().TrialStartDate);
        Assert.NotNull(response.Data.Items.First().TrialEndDate);
        Assert.NotNull(response.Data.Items.First().StartDate);
    }

    [Test]
    public void Should_Update_Subscription_Card()
    {
        var randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var createProductResponse = Product.Create(createProductRequest, _options);

        var createPlanRequest = new CreatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 3,
            Price = "5.23",
            CurrencyCode = Currency.TRY.ToString(),
            PaymentInterval = PaymentInterval.WEEKLY.ToString(),
            RecurrenceCount = 12,
            PaymentIntervalCount = 1,
            PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
            ProductReferenceCode = createProductResponse.Data.ReferenceCode
        };

        var planResource = Plan.Create(createPlanRequest, _options).Data;

        var subscriptionInitializeRequest = new SubscriptionInitializeRequest
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
            PricingPlanReferenceCode = planResource.ReferenceCode,
            SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
        };

        var initializeResponse = Subscription.Initialize(subscriptionInitializeRequest, _options);

        var request = new UpdateCardRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            CustomerReferenceCode = initializeResponse.Data.CustomerReferenceCode,
            CallbackUrl = "https://www.google.com"
        };

        var response = Subscription.UpdateCard(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
        Assert.NotNull(response.CheckoutFormContent);
        Assert.NotNull(response.Token);
        Assert.NotNull(response.TokenExpireTime);
    }
}