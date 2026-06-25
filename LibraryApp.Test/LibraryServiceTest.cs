using LibraryApp.Exceptions;
using LibraryApp.Fakes;
using LibraryApp.Models;
using LibraryApp.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Test
{
    [TestFixture]
    public class LibraryServiceTest
    {
        private FakeBookService fakeBookService;
        private FakeDeliveryService fakeDeliveryService;
        private FakePurchaseService fakePurchaseService;

        private LibraryService libraryService;

        [SetUp]
        public void SetUp()
        {
            fakeBookService = new FakeBookService();
            fakeDeliveryService = new FakeDeliveryService();
            fakePurchaseService = new FakePurchaseService();

            libraryService = new LibraryService(fakeBookService, fakeDeliveryService, fakePurchaseService);
        }
        // Valid:
        // 1) Oversea and percentOfUnprocessedRequests > 80, NumberOfCopies >= 10, NumberOfCopiesToBePurchased = 20.
        // 2) Oversea and percentOfUnprocessedRequests > 80, NumberOfCopies < 10, NumberOfCopiesToBePurchased = 15.
        // 3) International, percentOfUnprocessedRequests > 50 or numOftotalRequests > 10, NumberOfCopiesToBePurchased = 15.
        // 4) International, percentOfUnprocessedRequests < 50 and numOftotalRequests < 10, NumberOfCopiesToBePurchased = 12.
        // 5) Every other event the result is NumberOfCopiesToBePurchased = 10.
        // ------------------------------------------------------------------------------------------------------------------
        // Using:
        // Oversea, percentOfUnprocessedRequests = 81, NumberOfCopies = 10
        // ------------------------------------------------------------------------------------------------------------------
        // Invalid:
        // Not DeliveryType, negative number, NAN.
        [Test]
        public void DoPurchaseCalculation_ShouldRequestDelivery_Success()
        {
            // Arrange
            fakeBookService.bookRequest = new BookRequestInfo();
            fakeBookService.bookRequest.PercentOfUnprocessedRequests = 81;
            fakeDeliveryService.deliveryType = new DeliveryType();
            fakeDeliveryService.deliveryType = DeliveryType.Oversea;

            Book book = new Book();
            book.NumberOfCopies = 10;

            // Act and Assert
            Assert.That(book.NumberOfCopies, Is.EqualTo(fakePurchaseService.purchaseLog[0].NumberOfCopiesToBePurchased));
        }

        [Test]
        public void DoPurchaseCalculation_ShouldRequestDelivery_Exception()
        {
            // Arrange
            fakeBookService.isException = new NoRequestsForCalculationException();
            Book book = new Book();

            // Act
            NoRequestsForCalculationException exception = Assert.Throws<NoRequestsForCalculationException>((TestDelegate)(() => libraryService.DoPurchaseCalculation(book)));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(fakePurchaseService.purchaseLog[0]));
        }

        // Valid:
        // 1) High, numOfPurchasesInTheLastMonth >= 5 and bookPrice > 10.0, penalty = true, return 20.
        // 2) High, numOfPurchasesInTheLastMonth >= 5 and bookPrice > 10.0, penalty = false, return 25.
        // 3) High, numOfPurchasesInTheLastMonth < 5 or bookPrice < 10.0, penalty = true, return 10.
        // 4) Regular, numOfPurchasesInTheLastMonth > 12 or bookPrice > 25.0, return 15.
        // 5) Regular, numOfPurchasesInTheLastMonth < 12 AND bookPrice < 25.0, return 10.
        // 6) Low, return 0.
        // Invalid:
        // Not ActivityFrequency, negative numbers, NAN
        [TestCaseSource(typeof(PICTParser), nameof(PICTParser.GetTestCase))]
        public void GetMemberDiscount_PictParser(double bookPrice, int numOfPurchasesInTheLastMonth, bool penalty, ActivityFrequency activityFrequency, int expected_result)
        {
            // Act and Assert
            int result = libraryService.GetMemberDiscount(bookPrice, numOfPurchasesInTheLastMonth, penalty, activityFrequency);

            Assert.That(expected_result, Is.EqualTo(result));
        }
    }
}
