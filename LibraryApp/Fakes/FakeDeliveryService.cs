using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Models;
using LibraryApp.Services;
using LibraryApp.Exceptions;
using LibraryApp;

namespace LibraryApp.Fakes
{
    public class FakeDeliveryService : IDeliveryService
    {
        public DeliveryType deliveryType {  get; set; }
        public NoRequestsForCalculationException isException { get; set; }

        public DeliveryType GetDeliveryTypeForBook(Guid bookId)
        {
            if (isException != null)
            {
                throw new NoRequestsForCalculationException("[DoPurchaseCalculation] Book wasn't requested in the last month.");
            }
            return deliveryType;
        }
    }
}
