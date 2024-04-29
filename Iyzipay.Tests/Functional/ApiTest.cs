using Iyzicore.Model;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class ApiTest : BaseTest
{
    [Test]
    public void Should_Test_Api()
    {
        var iyzipayResource = Iyzicore.Model.ApiTest.Retrieve(_options);

        PrintResponse(iyzipayResource);

        Assert.AreEqual(Status.SUCCESS.ToString(), iyzipayResource.Status);
        Assert.AreEqual(Locale.TR.ToString(), iyzipayResource.Locale);
        Assert.NotNull(iyzipayResource.SystemTime);
        Assert.Null(iyzipayResource.ErrorCode);
        Assert.Null(iyzipayResource.ErrorMessage);
        Assert.Null(iyzipayResource.ErrorGroup);
    }
}