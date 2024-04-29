using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Samples;

public class BinNumberSample : Sample
{
    [Test]
    public void Should_Retrieve_Bin_Number()
    {
        var request = new RetrieveBinNumberRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.BinNumber = "554960";

        var binNumber = BinNumber.Retrieve(request, options);

        PrintResponse(binNumber);

        Assert.AreEqual(Status.SUCCESS.ToString(), binNumber.Status);
        Assert.AreEqual(Locale.TR.ToString(), binNumber.Locale);
        Assert.AreEqual("123456789", binNumber.ConversationId);
        Assert.IsNotNull(binNumber.SystemTime);
        Assert.IsNull(binNumber.ErrorCode);
        Assert.IsNull(binNumber.ErrorMessage);
        Assert.IsNull(binNumber.ErrorGroup);
        Assert.AreEqual("554960", binNumber.Bin);
        Assert.AreEqual("CREDIT_CARD", binNumber.CardType);
        Assert.AreEqual("MASTER_CARD", binNumber.CardAssociation);
        Assert.AreEqual("Bonus", binNumber.CardFamily);
        Assert.AreEqual("Garanti Bankası", binNumber.BankName);
        Assert.AreEqual(62, binNumber.BankCode);
    }
}