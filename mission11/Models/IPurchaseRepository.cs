using System;
using System.Linq;

namespace mission11.Models
{
	public interface IPurchaseRepository
	{
        IQueryable<Purchase> Purchases { get; }

        void SavePurchase(Purchase purchase);
    }
}

