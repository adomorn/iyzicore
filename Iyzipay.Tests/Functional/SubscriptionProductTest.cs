using System;
using Iyzicore;
using Iyzicore.Model;
using Iyzicore.Model.V2.Subscription;
using Iyzicore.Request.V2.Subscription;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class SubscriptionProductTest : BaseTest
{
    [Test]
    public void Should_Create_Product()
    {
        var randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        var request = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var response = Product.Create(request, _options);
        PrintResponse(response);

        Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
        Assert.AreEqual($"product-name-{randomString}", response.Data.Name);
        Assert.AreEqual("product-description", response.Data.Description);
        Assert.IsNotNull(response.Data.ReferenceCode);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Update_Product()
    {
        var randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var productResource = Product.Create(createProductRequest, _options).Data;

        var updateProductRequest = new UpdateProductRequest
        {
            Description = "updated-description",
            Locale = Locale.TR.ToString(),
            Name = $"updated-product-name-{randomString}",
            ConversationId = "123456789",
            ProductReferenceCode = productResource.ReferenceCode
        };

        var response = Product.Update(updateProductRequest, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual($"updated-product-name-{randomString}", response.Data.Name);
        Assert.AreEqual("updated-description", response.Data.Description);
        Assert.AreEqual(productResource.ReferenceCode, response.Data.ReferenceCode);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Delete_Product()
    {
        var randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var productResource = Product.Create(createProductRequest, _options).Data;

        var updateProductRequest = new DeleteProductRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            ProductReferenceCode = productResource.ReferenceCode
        };

        var response = Product.Delete(updateProductRequest, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_Retrieve_Product()
    {
        var randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        var productResource = Product.Create(createProductRequest, _options).Data;

        var retrieveProductRequest = new RetrieveProductRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            ProductReferenceCode = productResource.ReferenceCode
        };

        var response = Product.Retrieve(retrieveProductRequest, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual($"product-name-{randomString}", response.Data.Name);
        Assert.AreEqual("product-description", response.Data.Description);
        Assert.IsNotNull(response.Data.ReferenceCode);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }

    [Test]
    public void Should_RetrieveAll_Product()
    {
        var randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        var createProductRequest = new CreateProductRequest
        {
            Description = "product-description",
            Locale = Locale.TR.ToString(),
            Name = $"product-name-{randomString}",
            ConversationId = "123456789"
        };

        Product.Create(createProductRequest, _options);
        var pagingRequest = new PagingRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            Page = 1,
            Count = 1
        };

        var response = Product.RetrieveAll(pagingRequest, _options);
        PrintResponse(response);

        Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
        Assert.AreEqual(1, response.Data.Items.Count);
        Assert.AreEqual(1, response.Data.CurrentPage);
        Assert.IsNotNull(response.SystemTime);
        Assert.Null(response.ErrorMessage);
    }
}