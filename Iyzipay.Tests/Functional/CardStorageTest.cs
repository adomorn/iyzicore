using Iyzicore.Model;
using Iyzicore.Request;
using Iyzipay.Tests.Functional.Builder;
using Iyzipay.Tests.Functional.Builder.Request;
using Iyzipay.Tests.Functional.Util;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class CardStorageTest : BaseTest
{
    [Test]
    public void Should_Create_User_And_Add_Card()
    {
        var externalUserId = RandomGenerator.RandomId;
        var cardInformation = CardInformationBuilder
            .Create()
            .Build();

        var createCardRequest = CreateCardRequestBuilder.Create()
            .Card(cardInformation)
            .ExternalId(externalUserId)
            .Email("email@email.com")
            .Build();

        var card = Card.Create(createCardRequest, _options);

        PrintResponse(card);

        Assert.AreEqual(Locale.TR.ToString(), card.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
        Assert.NotNull(card.SystemTime);
        Assert.AreEqual("123456789", card.ConversationId);
        Assert.AreEqual("email@email.com", card.Email);
        Assert.AreEqual("55287900", card.BinNumber);
        Assert.AreEqual("card alias", card.CardAlias);
        Assert.AreEqual("CREDIT_CARD", card.CardType);
        Assert.AreEqual("MASTER_CARD", card.CardAssociation);
        Assert.AreEqual("Paraf", card.CardFamily);
        Assert.AreEqual("Halk Bankası", card.CardBankName);
        Assert.True(card.CardBankCode.Equals(12L));
    }

    [Test]
    public void Should_Create_Card_And_Add_Card_To_Existing_User()
    {
        var externalUserId = RandomGenerator.RandomId;
        var cardInformation = CardInformationBuilder.Create()
            .Build();

        var cardRequest = CreateCardRequestBuilder.Create()
            .Card(cardInformation)
            .ExternalId(externalUserId)
            .Email("email@email.com")
            .Build();

        var firstCard = Card.Create(cardRequest, _options);
        var cardUserKey = firstCard.CardUserKey;

        var request = CreateCardRequestBuilder.Create()
            .Card(cardInformation)
            .CardUserKey(cardUserKey)
            .ExternalId(externalUserId)
            .Build();

        var card = Card.Create(request, _options);

        PrintResponse(card);

        Assert.AreEqual(Locale.TR.ToString(), card.Locale);
        Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
        Assert.NotNull(card.SystemTime);
        Assert.AreEqual("123456789", card.ConversationId);
        Assert.AreEqual("55287900", card.BinNumber);
        Assert.AreEqual("card alias", card.CardAlias);
        Assert.AreEqual("CREDIT_CARD", card.CardType);
        Assert.AreEqual("MASTER_CARD", card.CardAssociation);
        Assert.AreEqual("Paraf", card.CardFamily);
        Assert.AreEqual("Halk Bankası", card.CardBankName);
        Assert.AreEqual(externalUserId, card.ExternalId);
        Assert.True(card.CardBankCode.Equals(12L));
    }

    [Test]
    public void Should_Delete_Card()
    {
        var card = CreateCard();

        var deleteCardRequest = new DeleteCardRequest();
        deleteCardRequest.CardToken = card.CardToken;
        deleteCardRequest.CardUserKey = card.CardUserKey;

        var deletedCard = Card.Delete(deleteCardRequest, _options);

        PrintResponse(deletedCard);

        Assert.AreEqual(Status.SUCCESS.ToString(), deletedCard.Status);
        Assert.AreEqual(Locale.TR.ToString(), deletedCard.Locale);
        Assert.NotNull(deletedCard.SystemTime);
        Assert.Null(deletedCard.ErrorCode);
        Assert.Null(deletedCard.ErrorMessage);
        Assert.Null(deletedCard.ErrorGroup);
        Assert.Null(deletedCard.BinNumber);
        Assert.Null(deletedCard.CardAlias);
        Assert.Null(deletedCard.CardType);
        Assert.Null(deletedCard.CardAssociation);
        Assert.Null(deletedCard.CardFamily);
        Assert.Null(deletedCard.CardBankName);
        Assert.Null(deletedCard.CardBankCode);
        Assert.Null(deletedCard.CardUserKey);
        Assert.Null(deletedCard.CardToken);
        Assert.Null(deletedCard.Email);
        Assert.Null(deletedCard.ExternalId);
    }

    [Test]
    public void Chould_Retrieve_Card()
    {
        var card = CreateCard();

        var request = new RetrieveCardListRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.CardUserKey = card.CardUserKey;

        var cardList = CardList.Retrieve(request, _options);

        PrintResponse(cardList);

        Assert.AreEqual(Status.SUCCESS.ToString(), cardList.Status);
        Assert.AreEqual(Locale.TR.ToString(), cardList.Locale);
        Assert.AreEqual("123456789", cardList.ConversationId);
        Assert.NotNull(cardList.SystemTime);
        Assert.Null(cardList.ErrorCode);
        Assert.Null(cardList.ErrorMessage);
        Assert.Null(cardList.ErrorGroup);
        Assert.NotNull(cardList.CardDetails);
        Assert.False(cardList.CardDetails.Count == 0);
        Assert.NotNull(cardList.CardUserKey);
    }

    private Card CreateCard()
    {
        var cardInformation = CardInformationBuilder.Create()
            .Build();

        var cardRequest = CreateCardRequestBuilder.Create()
            .Card(cardInformation)
            .Email("email@email.com")
            .Build();

        return Card.Create(cardRequest, _options);
    }
}