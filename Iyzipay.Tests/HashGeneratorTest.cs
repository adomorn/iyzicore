using Iyzicore;
using NUnit.Framework;

namespace Iyzipay.Tests;

public class HashGeneratorTest
{
    [Test]
    public void Should_Generate_Hash()
    {
        var expectedHash = "Cy84UuLZpfGhI7oaPD0Ckx1M0mo=";
        var generatedHash = HashGenerator.GenerateHash("apiKey", "secretKey", "random", new TestRequest());

        Assert.AreEqual(expectedHash, generatedHash);
    }
}

public class TestRequest : BaseRequest
{
    public override string ToPKIRequestString()
    {
        return "[data=value]";
    }
}