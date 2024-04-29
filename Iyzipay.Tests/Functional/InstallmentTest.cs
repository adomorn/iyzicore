﻿using Iyzicore.Model;
using Iyzicore.Request;
using NUnit.Framework;

namespace Iyzipay.Tests.Functional
{
    public class InstallmentTest : BaseTest
    {
        [Test]
        public void Should_Retrieve_Installments()
        {
            RetrieveInstallmentInfoRequest request = new RetrieveInstallmentInfoRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.BinNumber = "554960";
            request.Price = "100";

            InstallmentInfo installmentInfo = InstallmentInfo.Retrieve(request, _options);

            PrintResponse(installmentInfo);

            Assert.AreEqual(Status.SUCCESS.ToString(), installmentInfo.Status);
            Assert.AreEqual(Locale.TR.ToString(), installmentInfo.Locale);
            Assert.AreEqual("123456789", installmentInfo.ConversationId);
            Assert.NotNull(installmentInfo.InstallmentDetails[0]);
            Assert.AreEqual("554960", installmentInfo.InstallmentDetails[0].BinNumber);
            Assert.AreEqual("100", installmentInfo.InstallmentDetails[0].Price);
            Assert.AreEqual("CREDIT_CARD", installmentInfo.InstallmentDetails[0].CardType);
            Assert.AreEqual("MASTER_CARD", installmentInfo.InstallmentDetails[0].CardAssociation);
            Assert.AreEqual("Bonus", installmentInfo.InstallmentDetails[0].CardFamilyName);
            Assert.AreEqual(0, installmentInfo.InstallmentDetails[0].Commercial);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].InstallmentNumber);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].Price);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].TotalPrice);
            Assert.NotNull(installmentInfo.SystemTime);
            Assert.Null(installmentInfo.ErrorCode);
            Assert.Null(installmentInfo.ErrorMessage);
            Assert.Null(installmentInfo.ErrorGroup);
            Assert.NotNull(installmentInfo.InstallmentDetails);
            Assert.False(installmentInfo.InstallmentDetails.Count == 0);
        }
        
        [Test]
        public void Should_Retrieve_Installments_With_Commercial_One()
        {
            RetrieveInstallmentInfoRequest request = new RetrieveInstallmentInfoRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.BinNumber = "552659";
            request.Price = "100";

            InstallmentInfo installmentInfo = InstallmentInfo.Retrieve(request, _options);

            PrintResponse(installmentInfo);

            Assert.AreEqual(Status.SUCCESS.ToString(), installmentInfo.Status);
            Assert.AreEqual(Locale.TR.ToString(), installmentInfo.Locale);
            Assert.AreEqual("123456789", installmentInfo.ConversationId);
            Assert.NotNull(installmentInfo.InstallmentDetails[0]);
            Assert.AreEqual("552659", installmentInfo.InstallmentDetails[0].BinNumber);
            Assert.AreEqual("100", installmentInfo.InstallmentDetails[0].Price);
            Assert.AreEqual("CREDIT_CARD", installmentInfo.InstallmentDetails[0].CardType);
            Assert.AreEqual("MASTER_CARD", installmentInfo.InstallmentDetails[0].CardAssociation);
            Assert.AreEqual("World", installmentInfo.InstallmentDetails[0].CardFamilyName);
            Assert.AreEqual(1, installmentInfo.InstallmentDetails[0].Commercial);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].InstallmentNumber);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].Price);
            Assert.NotNull(installmentInfo.InstallmentDetails[0].InstallmentPrices[0].TotalPrice);
            Assert.NotNull(installmentInfo.SystemTime);
            Assert.Null(installmentInfo.ErrorCode);
            Assert.Null(installmentInfo.ErrorMessage);
            Assert.Null(installmentInfo.ErrorGroup);
            Assert.NotNull(installmentInfo.InstallmentDetails);
            Assert.False(installmentInfo.InstallmentDetails.Count == 0);
        }
    }
}
