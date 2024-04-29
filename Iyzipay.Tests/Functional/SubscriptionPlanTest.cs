using System;
using Iyzicore.Model;
using Iyzicore.Model.V2.Subscription;
using Iyzicore.Request.V2.Subscription;
using Iyzipay.Tests.Functional.Util;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class SubscriptionPlanTest : BaseTest
{
    [Test]
    public void Should_Create_Plan()
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

        var request = new CreatePlanRequest
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

        var response = Plan.Create(request, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual($"plan-name-{randomString}", response.Data.Name);
        Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
        Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
        Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
        Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
        Assert.AreEqual(1, response.Data.PaymentIntervalCount);
        Assert.AreEqual(3, response.Data.TrialPeriodDays);
        Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
        Assert.AreEqual(12, response.Data.RecurrenceCount);
        Assert.AreEqual("ACTIVE", response.Data.Status);
        Assert.IsNotNull(response.Data.ReferenceCode);
        Assert.IsNotNull(response.Data.CreatedDate);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Update_Plan()
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

        var createPlanResponse = Plan.Create(createPlanRequest, _options);

        var request = new UpdatePlanRequest
        {
            Locale = Locale.TR.ToString(),
            Name = $"updated-plan-name-{randomString}",
            ConversationId = "123456789",
            TrialPeriodDays = 5,
            PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
        };

        var response = Plan.Update(request, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual($"updated-plan-name-{randomString}", response.Data.Name);
        Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
        Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
        Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
        Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
        Assert.AreEqual(1, response.Data.PaymentIntervalCount);
        Assert.AreEqual(5, response.Data.TrialPeriodDays);
        Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
        Assert.AreEqual(12, response.Data.RecurrenceCount);
        Assert.AreEqual("ACTIVE", response.Data.Status);
        Assert.IsNotNull(response.Data.ReferenceCode);
        Assert.IsNotNull(response.Data.CreatedDate);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Delete_Plan()
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

        var createPlanResponse = Plan.Create(createPlanRequest, _options);

        var request = new DeletePlanRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
        };

        var response = Plan.Delete(request, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Retrieve_Plan()
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

        var createPlanResponse = Plan.Create(createPlanRequest, _options);

        var request = new RetrievePlanRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
        };

        var response = Plan.Retrieve(request, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual($"plan-name-{randomString}", response.Data.Name);
        Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
        Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
        Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
        Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
        Assert.AreEqual(1, response.Data.PaymentIntervalCount);
        Assert.AreEqual(3, response.Data.TrialPeriodDays);
        Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
        Assert.AreEqual(12, response.Data.RecurrenceCount);
        Assert.AreEqual("ACTIVE", response.Data.Status);
        Assert.IsNotNull(response.Data.ReferenceCode);
        Assert.IsNotNull(response.Data.CreatedDate);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_RetrieveAll_Plan()
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

        Plan.Create(createPlanRequest, _options);

        var request = new RetrieveAllPlanRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            ProductReferenceCode = createProductResponse.Data.ReferenceCode,
            Count = 1,
            Page = 1
        };

        var response = Plan.RetrieveAll(request, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual(1, response.Data.Items.Count);
        Assert.AreEqual(1, response.Data.CurrentPage);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }
}