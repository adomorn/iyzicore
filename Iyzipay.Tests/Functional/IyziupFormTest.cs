using System.Collections.Generic;
using Iyzicore.Model;
using Iyzipay.Tests.Functional.Builder;
using Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional;

public class IyziupFormTest : BaseTest
{
    [Test]
    public void Should_Initialize_Iyziup_Form_For_Standard_Merchant()
    {
        var orderItems =
            new List<OrderItem>(new List<OrderItem>
            {
                OrderItemBuilder.Create().Price("0.3").Build()
            });

        var request = CreateIyziupFormInitializeRequestBuilder.Create()
            .Price("0.3")
            .PaymentGroup(PaymentGroup.LISTING.ToString())
            .PaidPrice("0.4")
            .CallbackUrl("https://www.merchant.com/callback")
            .OrderItems(orderItems)
            .Build();
    }
}