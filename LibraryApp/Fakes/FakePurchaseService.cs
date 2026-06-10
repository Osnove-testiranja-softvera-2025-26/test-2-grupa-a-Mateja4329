using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Fakes
{
    public class FakePurchaseService : IPurchaseService
    {
        List<string> purchaseLog = new List<string>();
        public void CreatePurchase(Purchase purchase)
        {
            purchaseLog.Add(purchase);
        }
    }
}
