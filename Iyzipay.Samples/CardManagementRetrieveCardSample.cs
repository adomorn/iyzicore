using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class CardManagementRetrieveCardSample : Sample
{
    [SetUp]
    public void SetUp()
    {
        Initialize();
        options.BaseUrl = "https://sandbox-cm.iyzipay.com";
    }

    [Test]
    public void Should_Retrieve_Card_Management_Page_Cards()
    {
        var retrieveCardRequest = new RetrieveCardManagementPageCardRequest();
        retrieveCardRequest.PageToken = "set page token";
        retrieveCardRequest.Locale = Locale.TR.ToString();
        retrieveCardRequest.ConversationId = "123456";

        var cardManagementPageCard = CardManagementPageCard.Retrieve(retrieveCardRequest, options);
        PrintResponse(cardManagementPageCard);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardManagementPageCard.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardManagementPageCard.Locale);
        Assert.NotNull(cardManagementPageCard.SystemTime);
        Assert.Null(cardManagementPageCard.ErrorCode);
        Assert.Null(cardManagementPageCard.ErrorMessage);
        Assert.Null(cardManagementPageCard.ErrorGroup);
        Assert.NotNull(cardManagementPageCard);
    }
}