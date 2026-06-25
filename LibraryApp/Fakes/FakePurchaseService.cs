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
    public class FakePurchaseService : IPurchaseService
    {
        public List<Purchase> purchaseLog = new List<Purchase>();
        public void CreatePurchase(Purchase purchase)
        {
            purchaseLog.Add(purchase);
        }
    }
}
