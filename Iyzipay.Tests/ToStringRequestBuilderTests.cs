using Iyzicore;
using Iyzicore.Model;
using NUnit.Framework;

namespace Iyzipay.Tests;

public class ToStringRequestBuilderTests
{
    [Test]
    public void Should_Append_And_Convert_Object_To_String()
    {
        var requestString = ToStringRequestBuilder.NewInstance()
            .Append("conversationId", "123456")
            .Append("locale", Locale.TR.ToString())
            .Append("price", "1.0").GetRequestString();

        Assert.AreEqual("[conversationId=123456,locale=tr,price=1.0]", requestString);
    }

    [Test]
    public void Should_Convert_To_Nothing()
    {
        var requestString = ToStringRequestBuilder.NewInstance().GetRequestString();
        Assert.AreEqual("[]", requestString);
    }
}