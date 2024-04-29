using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class CardStorageSample : Sample
{
    [Test]
    public void Should_Create_User_And_Add_Card()
    {
        var request = new CreateCardRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.Email = "email@email.com";
        request.ExternalId = "external id";

        var cardInformation = new CardInformation();
        cardInformation.CardAlias = "card alias";
        cardInformation.CardHolderName = "John Doe";
        cardInformation.CardNumber = "5528790000000008";
        cardInformation.ExpireMonth = "12";
        cardInformation.ExpireYear = "2030";
        request.Card = cardInformation;

        var card = Card.Create(request, options);

        PrintResponse(card);

        Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
        Assert.AreEqual(Locale.TR.ToString(), card.Locale);
        Assert.AreEqual("123456789", card.ConversationId);
        Assert.IsNotNull(card.SystemTime);
        Assert.IsNull(card.ErrorCode);
        Assert.IsNull(card.ErrorMessage);
        Assert.IsNull(card.ErrorGroup);
        Assert.AreEqual("55287900", card.BinNumber);
        Assert.AreEqual("card alias", card.CardAlias);
        Assert.AreEqual("CREDIT_CARD", card.CardType);
        Assert.AreEqual("MASTER_CARD", card.CardAssociation);
        Assert.AreEqual("Paraf", card.CardFamily);
        Assert.AreEqual("Halk Bankası", card.CardBankName);
        Assert.AreEqual(12, card.CardBankCode);
        Assert.IsNotNull(card.CardUserKey);
        Assert.IsNotNull(card.CardToken);
        Assert.AreEqual("email@email.com", card.Email);
        Assert.AreEqual("external id", card.ExternalId);
    }

    [Test]
    public void Should_Create_Card()
    {
        var request = new CreateCardRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardUserKey = "card user key";

        var cardInformation = new CardInformation();
        cardInformation.CardAlias = "card alias";
        cardInformation.CardHolderName = "John Doe";
        cardInformation.CardNumber = "5528790000000008";
        cardInformation.ExpireMonth = "12";
        cardInformation.ExpireYear = "2030";
        request.Card = cardInformation;

        var card = Card.Create(request, options);

        PrintResponse(card);

        Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
        Assert.AreEqual(Locale.TR.ToString(), card.Locale);
        Assert.AreEqual("123456789", card.ConversationId);
        Assert.IsNotNull(card.SystemTime);
        Assert.IsNull(card.ErrorCode);
        Assert.IsNull(card.ErrorMessage);
        Assert.IsNull(card.ErrorGroup);
        Assert.AreEqual("55287900", card.BinNumber);
        Assert.AreEqual("card alias", card.CardAlias);
        Assert.AreEqual("CREDIT_CARD", card.CardType);
        Assert.AreEqual("MASTER_CARD", card.CardAssociation);
        Assert.AreEqual("Paraf", card.CardFamily);
        Assert.AreEqual("Halk Bankası", card.CardBankName);
        Assert.AreEqual(12, card.CardBankCode);
        Assert.IsNotNull(card.CardUserKey);
        Assert.IsNotNull(card.CardToken);
        Assert.IsNull(card.Email);
        Assert.IsNull(card.ExternalId);
    }

    [Test]
    public void Should_Delete_Card()
    {
        var request = new DeleteCardRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardToken = "card token";
        request.CardUserKey = "card user key";

        var card = Card.Delete(request, options);

        PrintResponse(card);

        Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
        Assert.AreEqual(Locale.TR.ToString(), card.Locale);
        Assert.AreEqual("123456789", card.ConversationId);
        Assert.IsNotNull(card.SystemTime);
        Assert.IsNull(card.ErrorCode);
        Assert.IsNull(card.ErrorMessage);
        Assert.IsNull(card.ErrorGroup);
        Assert.IsNull(card.BinNumber);
        Assert.IsNull(card.CardAlias);
        Assert.IsNull(card.CardType);
        Assert.IsNull(card.CardAssociation);
        Assert.IsNull(card.CardFamily);
        Assert.IsNull(card.CardBankName);
        Assert.IsNull(card.CardBankCode);
        Assert.IsNull(card.CardUserKey);
        Assert.IsNull(card.CardToken);
        Assert.IsNull(card.Email);
        Assert.IsNull(card.ExternalId);
    }

    [Test]
    public void Should_Retrieve_Cards()
    {
        var request = new RetrieveCardListRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardUserKey = "card user key";

        var cardList = CardList.Retrieve(request, options);

        PrintResponse(cardList);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardList.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardList.Locale);
        Assert.AreEqual("123456789", cardList.ConversationId);
        Assert.IsNotNull(cardList.SystemTime);
        Assert.IsNull(cardList.ErrorCode);
        Assert.IsNull(cardList.ErrorMessage);
        Assert.IsNull(cardList.ErrorGroup);
        Assert.IsNotNull(cardList.CardDetails);
        Assert.IsNotEmpty(cardList.CardDetails);
        Assert.IsNotNull(cardList.CardUserKey);
    }
}