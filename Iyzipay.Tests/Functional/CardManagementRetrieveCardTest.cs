using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class CardManagementRetrieveCardTest : BaseTest
{
    [SetUp]
    public void SetUp()
    {
        Initialize();
        _options.BaseUrl = "https://sandbox-cm.iyzipay.com";
    }

    [Test]
    public void Should_Retrieve_Cards()
    {
        var initializeRequest = CardManagementPageRequestBuilder.Create().Build();
        var cardManagementPageInitialize = CardManagementPageInitialize.Create(initializeRequest, _options);

        var retrieveCardRequest = CardManagementRetrieveCardBuilder.Create()
            .PageToken(cardManagementPageInitialize.Token)
            .Build();

        var cardManagementPageCard = CardManagementPageCard.Retrieve(retrieveCardRequest, _options);
        PrintResponse(cardManagementPageCard);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardManagementPageCard.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardManagementPageCard.Locale);
        Assert.Null(cardManagementPageCard.ErrorCode);
        Assert.Null(cardManagementPageCard.ErrorMessage);
        Assert.Null(cardManagementPageCard.ErrorGroup);
        Assert.NotNull(cardManagementPageCard);
    }

    [Test]
    public void Should_Not_Retrieve_Cards_When_PageToken_Is_Not_Exist()
    {
        var retrieveCardRequest = CardManagementRetrieveCardBuilder.Create()
            .PageToken("pagetoken")
            .Build();

        var cardManagementPageCard = CardManagementPageCard.Retrieve(retrieveCardRequest, _options);
        PrintResponse(cardManagementPageCard);

        Assert.AreEqual(Status.FAILURE.ToString(), cardManagementPageCard.Status);
        Assert.AreEqual("4002", cardManagementPageCard.ErrorCode);
        Assert.AreEqual("Geçersiz token", cardManagementPageCard.ErrorMessage);
    }
}