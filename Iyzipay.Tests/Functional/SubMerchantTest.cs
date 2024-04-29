﻿using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder.Request;
using Iyzipay.Tests.Functional.Util;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class SubMerchantTest : BaseTest
{
    [Test]
    public void Should_Create_personal_Sub_Merchant()
    {
        var subMerchantExternalId = RandomGenerator.RandomId;
        var request = CreateSubMerchantRequestBuilder.Create()
            .SubMerchantType(SubMerchantType.PERSONAL.ToString())
            .ContactName("John")
            .ContactSurname("Doe")
            .IdentityNumber("123456789")
            .SubMerchantExternalId(subMerchantExternalId)
            .Build();

        var subMerchant = SubMerchant.Create(request, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Create_Private_Sub_Merchant()
    {
        var subMerchantExternalId = RandomGenerator.RandomId;
        var request = CreateSubMerchantRequestBuilder.Create()
            .SubMerchantType(SubMerchantType.PRIVATE_COMPANY.ToString())
            .LegalCompanyTitle("John Doe inc")
            .TaxOffice("Tax office")
            .IdentityNumber("31300864726")
            .SubMerchantExternalId(subMerchantExternalId)
            .Build();

        var subMerchant = SubMerchant.Create(request, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Create_Limited_Company_Sub_Merchant()
    {
        var subMerchantExternalId = RandomGenerator.RandomId;
        var request = CreateSubMerchantRequestBuilder.Create()
            .SubMerchantExternalId(subMerchantExternalId)
            .SubMerchantType(SubMerchantType.LIMITED_OR_JOINT_STOCK_COMPANY.ToString())
            .TaxOffice("Tax office")
            .TaxNumber("9261877")
            .LegalCompanyTitle("XYZ inc")
            .Build();

        var subMerchant = SubMerchant.Create(request, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Update_Personal_Sub_Merchant()
    {
        var createPersonalSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .PersonalSubMerchantRequest()
            .Build();

        var subMerchantKey = SubMerchant.Create(createPersonalSubMerchantRequest, _options).SubMerchantKey;

        var updateSubMerchantRequest = UpdateSubMerchantRequestBuilder.Create()
            .SubMerchantKey(subMerchantKey)
            .ContactName("Jane")
            .ContactSurname("Doe")
            .IdentityNumber("31300864726")
            .Name("Jane's market")
            .Build();

        var subMerchant = SubMerchant.Update(updateSubMerchantRequest, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Update_Private_Sub_Merchant()
    {
        var createPrivateSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .PrivateSubMerchantRequest()
            .Build();

        var subMerchantKey = SubMerchant.Create(createPrivateSubMerchantRequest, _options).SubMerchantKey;

        var updateSubMerchantRequest = UpdateSubMerchantRequestBuilder.Create()
            .SubMerchantKey(subMerchantKey)
            .IdentityNumber("31300864726")
            .TaxOffice("Tax office")
            .LegalCompanyTitle("Jane Doe inc")
            .Build();

        var subMerchant = SubMerchant.Update(updateSubMerchantRequest, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Update_Limited_Company_Sub_Merchant()
    {
        var createLimitedCompanySubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .LimitedCompanySubMerchantRequest()
            .Build();

        var subMerchantKey = SubMerchant.Create(createLimitedCompanySubMerchantRequest, _options).SubMerchantKey;

        var updateSubMerchantRequest = UpdateSubMerchantRequestBuilder.Create()
            .SubMerchantKey(subMerchantKey)
            .Name("Jane's market")
            .IdentityNumber("31300864726")
            .TaxOffice("Tax office")
            .LegalCompanyTitle("ABC inc")
            .Build();

        var subMerchant = SubMerchant.Update(updateSubMerchantRequest, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }

    [Test]
    public void Should_Retrieve_Sub_Merchant()
    {
        var subMerchantExternalId = RandomGenerator.RandomId;
        var createLimitedCompanySubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
            .LimitedCompanySubMerchantRequest()
            .SubMerchantExternalId(subMerchantExternalId)
            .Build();

        SubMerchant.Create(createLimitedCompanySubMerchantRequest, _options);

        var request = RetrieveSubMerchantRequestBuilder.Create()
            .SubMerchantExternalId(subMerchantExternalId)
            .Build();

        var subMerchant = SubMerchant.Retrieve(request, _options);

        PrintResponse(subMerchant);

        Assert.AreEqual(Status.SUCCESS.ToString(), subMerchant.Status);
        Assert.AreEqual(Locale.TR.ToString(), subMerchant.Locale);
        Assert.AreEqual("123456789", subMerchant.ConversationId);
        Assert.AreEqual("9261877", subMerchant.TaxNumber);
        Assert.AreEqual("TR180006200119000006672315", subMerchant.Iban);
        Assert.AreEqual("Tax office", subMerchant.TaxOffice);
        Assert.AreEqual(subMerchantExternalId, subMerchant.SubMerchantExternalId);
        Assert.NotNull(subMerchant.SystemTime);
        Assert.Null(subMerchant.ErrorCode);
        Assert.Null(subMerchant.ErrorMessage);
        Assert.Null(subMerchant.ErrorGroup);
    }
}