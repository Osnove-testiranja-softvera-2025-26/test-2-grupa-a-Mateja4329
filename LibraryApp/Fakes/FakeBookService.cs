using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Fakes
{
    public class FakeBookService : IBookService
    {
        public BookRequestInfo bookRequest {  get; set; }
        public NoRequestsForCalculationException isException { get; set; }

        public BookRequestInfo GetBookRequestsInTheLastMonthInfo(Guid bookId)
        {
            if(isException)
            {
                throw new NoRequestsForCalculationException("[DoPurchaseCalculation] Book wasn't requested in the last month.");
            }
            return bookRequest;
        }
    }
}
