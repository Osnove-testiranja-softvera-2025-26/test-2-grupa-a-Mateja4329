using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Fakes
{
    public class FakeDeliveryService : IDeliveryService
    {
        public DeliveryType deliveryType {  get; set; }
        public NoRequestsForCalculationException isException { get; set; }

        public DeliveryType GetDeliveryTypeForBook(Guid bookId)
        {
            if (isException)
            {
                throw new NoRequestsForCalculationException("[DoPurchaseCalculation] Book wasn't requested in the last month.");
            }
            return deliveryType;
        }
    }
}
