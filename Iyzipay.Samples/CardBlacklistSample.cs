using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class CardBlacklistSample : Sample
{
    [Test]
    public void Should_Create_Card_Blacklist()
    {
        var request = new CreateCardBlacklistRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardToken = "";
        request.CardUserKey = "";


        var cardBlacklist = CardBlacklist.Create(request, options);


        PrintResponse(cardBlacklist);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardBlacklist.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardBlacklist.Locale);
        Assert.AreEqual("123456789", cardBlacklist.ConversationId);
        Assert.IsNotNull(cardBlacklist.SystemTime);
        Assert.IsNull(cardBlacklist.ErrorCode);
        Assert.IsNull(cardBlacklist.ErrorMessage);
        Assert.IsNull(cardBlacklist.ErrorGroup);
        Assert.IsNotNull(cardBlacklist.CardUserKey);
        Assert.IsNotNull(cardBlacklist.CardToken);
    }

    [Test]
    public void Should_Update_Card_Blacklist()
    {
        var request = new UpdateCardBlacklistRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardToken = "";
        request.CardUserKey = "";


        var cardBlacklist = CardBlacklist.Update(request, options);


        PrintResponse(cardBlacklist);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardBlacklist.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardBlacklist.Locale);
        Assert.AreEqual("123456789", cardBlacklist.ConversationId);
        Assert.IsNotNull(cardBlacklist.SystemTime);
        Assert.IsNull(cardBlacklist.ErrorCode);
        Assert.IsNull(cardBlacklist.ErrorMessage);
        Assert.IsNull(cardBlacklist.ErrorGroup);
        Assert.IsNotNull(cardBlacklist.CardUserKey);
        Assert.IsNotNull(cardBlacklist.CardToken);
    }

    [Test]
    public void Should_Retrieve_Blacklist_Cards()
    {
        var request = new RetrieveCardBlacklistRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardNumber = "";

        var cardBlacklist = CardBlacklist.Retrieve(request, options);

        PrintResponse(cardBlacklist);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardBlacklist.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardBlacklist.Locale);
        Assert.IsNotNull(cardBlacklist.SystemTime);
        Assert.IsNull(cardBlacklist.ErrorCode);
        Assert.IsNull(cardBlacklist.ErrorMessage);
        Assert.IsNull(cardBlacklist.ErrorGroup);
        Assert.IsNotNull(cardBlacklist.CardNumber);
        Assert.IsNotNull(cardBlacklist.Blacklisted);
    }
}